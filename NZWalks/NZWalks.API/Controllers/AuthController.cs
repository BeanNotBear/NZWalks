using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Services;
using NZWalks.API.Services.Implements;
using Microsoft.AspNetCore.Authorization;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{

		private readonly UserManager<IdentityUser> userManager;
		private readonly ITokenRepository tokenRepository;
		private readonly IEmailService emailService;

		public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, IEmailService emailService)
		{
			this.userManager = userManager;
			this.tokenRepository = tokenRepository;
			this.emailService = emailService;
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
						await SendConfirmationEmail(registerRequestDto.Username, identityUser);
						return Ok("User was registered! Please login.");
					}
				}

			}
			return BadRequest("Something went wrong!");
		}

		private async Task SendConfirmationEmail(string? email, IdentityUser? user)
		{
			var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
			var confirmationLink = $"https://localhost:7076/api/Auth/ConfirmEmail?UserId={user.Id}&Token={token}";
			await emailService.SendEmailAsync(email, "Confirm Your Email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>;.");
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
					var isConfirm = await userManager.IsEmailConfirmedAsync(user);

					if (isConfirm)
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
					else
					{
						await SendConfirmationEmail(user.Email, user);
						return Ok("Email must be confirm first!");
					}


				}
			}

			return BadRequest("Username or password incorrect!");
		}

		[HttpGet]
		[Route("ConfirmEmail")]
		[AllowAnonymous]
		public async Task<IActionResult> ConfirmEmail(string userId, string token)
		{
			if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
			{
				return BadRequest("User ID and Token are required");
			}

			var user = await userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return BadRequest("Invalid User ID");
			}
			token = token.Replace(" ", "+");
			var result = await userManager.ConfirmEmailAsync(user, token);
			if (result.Succeeded)
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

			return BadRequest("Email confirmation failed");
		}
	}
}
