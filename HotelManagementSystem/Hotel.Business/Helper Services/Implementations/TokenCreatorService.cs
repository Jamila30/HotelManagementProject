using Hotel.Business.Helper_Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel.Business.Helper_Services.Implementations
{
	public class TokenCreatorService : ITokenCreatorService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration _configuration;
		public TokenCreatorService(UserManager<AppUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<TokenResponseDto> CreateTokenForUser(AppUser user, int minute)
		{
			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.Name,user.UserName),
				new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
				new Claim(ClaimTypes.NameIdentifier,user.Id),
				new Claim(ClaimTypes.Email,user.Email)
			};

			foreach (var item in await _userManager.GetRolesAsync(user))
			{
				claims.Add(new Claim(ClaimTypes.Role, item));
			}
			SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecurityKey"]));
			SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
			JwtSecurityToken jwtSecuritytoken = new
				(
				issuer: _configuration["JwtSettings:Issuer"],
				audience: _configuration["JwtSettings:Audience"],
				claims: claims,
				notBefore: DateTime.UtcNow,
				expires: DateTime.UtcNow.AddMinutes(3),
				signingCredentials: signingCredentials
				);


			JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
			var token = jwtSecurityTokenHandler.WriteToken(jwtSecuritytoken);

			return new TokenResponseDto()
			{
				Token = token,
				ExpireDate = jwtSecuritytoken.ValidTo,
				Username = user.UserName
			};

		}
	}
}
