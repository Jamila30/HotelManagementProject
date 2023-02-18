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


			if (!identityResult.Succeeded) throw new BadRequestException("Register didnt successfully");
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

			var response =await _tokenCreator.CreateTokenForUser(user, 10);
			return response;

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
			var decodeToken = HttpUtility.UrlDecode(token);
			var identityResult = await _userManager.ResetPasswordAsync(user, decodeToken, resetPassword.NewPassword);
			if (!identityResult.Succeeded) throw new BadRequestException("password couldn't reset!");
		}
	}
}
