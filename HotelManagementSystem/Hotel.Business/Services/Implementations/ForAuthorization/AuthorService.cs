using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel.Business.Services.Implementations.ForAuthorization
{
	public class AuthorService : IAuthorService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration _configuration;	
		public AuthorService(UserManager<AppUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}
		public async Task<GeneralResponseDto> RegisterAsync(RegisterDto register)
		{

			var userExist = await _userManager.FindByEmailAsync(register.Email);
			if (userExist != null) throw new AlreadyExistException("this email aready exist enter another email");
			AppUser user = new()
			{
				Email = register.Email,
				Fullname = register.Fullname,
				PhoneNumber = register.Phone,
				UserName = register.Username,
				EmailConfirmed = false
			};

			var identityResult = await _userManager.CreateAsync(user, register.Password);
			await _userManager.AddToRoleAsync(user, Roles.User.ToString());


			if (!identityResult.Succeeded) throw new BadRequestException("Register didnt successfully");
			var token = HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user));
			return new GeneralResponseDto()
			{
				Token = token,
				ExpireDate = DateTime.UtcNow.AddMinutes(3),
				Username = user.UserName,
				UserId = user.Id,
				Email = user.Email
			};
		}

	
		public async Task ConfirmEmail(string token, string userId)
		{
			if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId)) throw new BadRequestException("token or user id is invalid");
			var user = await _userManager.FindByIdAsync(userId);
			if (user.EmailConfirmed == true) throw new AlreadyExistException("This email already confirmed");
			var decodeToken = HttpUtility.UrlDecode(token);
			var result = await _userManager.ConfirmEmailAsync(user, decodeToken);
			if (!result.Succeeded)
			{
				throw new ConfirmationException("Email didnt confirmed!");
			}

		}

		public async Task<TokenResponseDto> LoginAsync(LoginDto login)
		{
			var user = await _userManager.FindByEmailAsync(login.Email);
			if (user is null) throw new NotFoundException("Username or Password are Invalid");
			if (user.EmailConfirmed == false) throw new ConfirmationException("This account didnt confirmed");
			var resultSign = await _userManager.CheckPasswordAsync(user, login.Password);
			if (!resultSign) throw new NotFoundException("Username or Password are Invalid");

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

		public async Task<GeneralResponseDto> ForgotPasswordAsync(ForgotPasswordDto forgotPassword)
		{
			var user = await _userManager.FindByEmailAsync(forgotPassword.EmailOrUsername);
			if (user is null)
			{
				user = await _userManager.FindByNameAsync(forgotPassword.EmailOrUsername);
			}

			if (user is null) throw new NotFoundException("there is not any account for this email/username");

			var token = HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user));
			return new GeneralResponseDto
			{
				Token = token,
				Email = user.Email,
				ExpireDate = DateTime.UtcNow.AddMinutes(3),
				UserId = user.Id,
				Username = user.UserName
			};
		}


		public async Task ResetPasswordAsync(string token, string userId, ResetPasswordDto resetPassword)
		{
			if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token)) throw new BadRequestException("token or email is empty");
			var user = await _userManager.FindByIdAsync(userId);
			if (user is null) throw new NotFoundException("User not found");
			var decodeToken = HttpUtility.UrlDecode(token);
			var identityResult = await _userManager.ResetPasswordAsync(user, decodeToken, resetPassword.NewPassword);
			if (!identityResult.Succeeded) throw new BadRequestException("password couldn't reset!");
		}
	}
}
