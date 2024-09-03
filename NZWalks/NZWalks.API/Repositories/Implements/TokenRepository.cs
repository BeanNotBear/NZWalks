﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repositories.Implements
{
	public class TokenRepository : ITokenRepository
	{

		private readonly IConfiguration configuration;

		public TokenRepository(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public string CreateJwtToken(IdentityUser user, List<string> Roles)
		{
			// Create claims
			var claims = new List<Claim>();

			claims.Add(new Claim(ClaimTypes.Email, user.Email));

			foreach (var role in Roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				configuration["Jwt:Issuer"],
				configuration["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddMinutes(15),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
