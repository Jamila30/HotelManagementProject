namespace Hotel.Business.Services.Implementations.ForAuthorization
{
	public class AuthService : IAutService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration _configuration;
		private readonly ITokenCreatorService _tokenCreator;
		public AuthService(UserManager<AppUser> userManager, IConfiguration configuration, ITokenCreatorService tokenCreator)
		{
			_userManager = userManager;
			_configuration = configuration;
			_tokenCreator = tokenCreator;
		}
		public async Task<GeneralResponseDto> RegisterAsync(RegisterDto register)
		{

			var userExist = await _userManager.FindByEmailAsync(register.Email);
			if (userExist != null) throw new AlreadyExistException("this email aready exist enter another email");
			AppUser user = new()
			{
				Email = register.Email,
				Fullname = register.Fullname,
				UserName = register.Username,
				EmailConfirmed = false
			};

			var identityResult = await _userManager.CreateAsync(user, register.Password);
			await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

			string errors = string.Empty;
			int count = 1;
			if (!identityResult.Succeeded)
			{
				foreach (var error in identityResult.Errors)
				{
					errors += count + "." + error.Description + "\n";
					count++;
				}
				throw new BadRequestException(errors.Trim());
			}
			var token = HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user));
			return new GeneralResponseDto()
			{
				Token = token,
				ExpireDate = DateTime.UtcNow.AddMinutes(10),
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
			var result = await _userManager.ConfirmEmailAsync(user, token);
			string errors = string.Empty;
			int count = 1;
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					errors += count + "." + error.Description + "\n";
					count++;
				}
				throw new ConfirmationException(errors.Trim());

			}
		}



		public async Task<TokenResponseDto> LoginAsync(LoginDto login)
		{
			var user = await _userManager.FindByEmailAsync(login.Email);
			if (user is null) throw new NotFoundException("Username or Password are Invalid");
			if (user.EmailConfirmed == false) throw new ConfirmationException("This account didnt confirmed");
			var resultSign = await _userManager.CheckPasswordAsync(user, login.Password);
			if (!resultSign) throw new NotFoundException("Username or Password are Invalid");

			var response = await _tokenCreator.CreateTokenForUser(user,20);
			var refreshToken = _tokenCreator.GenerateRefreshToken();

			_ = int.TryParse(_configuration["JwtSettings:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

			user.RefreshToken = refreshToken;
			user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

			await _userManager.UpdateAsync(user);

			return new TokenResponseDto
			{
				Token = response.Token,
				RefreshToken = refreshToken,
				ExpireDate = response.ExpireDate,
				Username = response.Username
			};

		}
		public async Task<Tokens> RefreshToken(Tokens tokenModel)
		{
			if (tokenModel is null)
			{
				throw new BadRequestException("Invalid client request");
			}

			string? accessToken = tokenModel.AccessToken;
			string? refreshToken = tokenModel.RefreshToken;

			var principal = _tokenCreator.GetPrincipalFromExpiredToken(accessToken);
			if (principal == null)
			{
				throw new BadRequestException("Invalid access token or refresh token");
			}
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
			string username = principal.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

			var user = await _userManager.FindByNameAsync(username);

			if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
			{
				throw new BadRequestException("Invalid access token or refresh token");
			}

			var newResponse = await _tokenCreator.CreateTokenForUser(user, 10);
			var newRefreshToken = _tokenCreator.GenerateRefreshToken();

			user.RefreshToken = newRefreshToken;
			await _userManager.UpdateAsync(user);
			return new Tokens
			{
				AccessToken = newResponse.Token,
				RefreshToken = newRefreshToken
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

			var token = HttpUtility.UrlEncode(await _userManager.GeneratePasswordResetTokenAsync(user));
			return new GeneralResponseDto
			{
				Token = token,
				Email = user.Email,
				ExpireDate = DateTime.UtcNow.AddMinutes(10),
				UserId = user.Id,
				Username = user.UserName
			};
		}


		public async Task ResetPasswordAsync(string token, string userId, ResetPasswordDto resetPassword)
		{
			if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token)) throw new BadRequestException("token or user id is empty");
			var user = await _userManager.FindByIdAsync(userId);
			if (user is null) throw new NotFoundException("User not found");
		   // var decodeToken = HttpUtility.UrlDecode(token);
			var identityResult = await _userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);

			string errors = string.Empty;
			int count = 1;
			if (!identityResult.Succeeded)
			{
				foreach (var error in identityResult.Errors)
				{
					errors += count + "." + error.Description + "\n";
					count++;
				}
				throw new BadRequestException(errors.Trim());
			}
		}

	}
}