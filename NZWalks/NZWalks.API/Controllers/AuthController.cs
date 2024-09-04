using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{

		private readonly UserManager<IdentityUser> userManager;
		private readonly ITokenRepository tokenRepository;

		public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
		{
			this.userManager = userManager;
			this.tokenRepository = tokenRepository;
		}

		// POST: /api/Auth/Register
		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
		{
			var identityUser = new IdentityUser
			{
				UserName = registerRequestDto.Username,
				Email = registerRequestDto.Username
			};

			var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

			if (identityResult.Succeeded)
			{
				// Add roles to this user
				if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
				{
					identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

					if (identityResult.Succeeded)
					{
						return Ok("User was registered! Please login.");
					}
				}

			}
			return BadRequest("Something went wrong!");
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
		{

			var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

			if (user != null)
			{
				var isCorrectPassword = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

				if (isCorrectPassword)
				{
					// Create token
					var roles = await userManager.GetRolesAsync(user);

					var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());

					var loginResponse = new LoginResponseDto
					{
						JwtToken = jwtToken
					};

					return Ok(loginResponse);
				}
			}

			return BadRequest("Username or password incorrect!");
		}

	}
}
