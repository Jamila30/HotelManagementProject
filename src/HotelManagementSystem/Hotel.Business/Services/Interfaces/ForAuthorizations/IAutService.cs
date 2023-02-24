using Microsoft.AspNetCore.Mvc;

namespace Hotel.Business.Services.Interfaces.ForAuthorizations
{
    public interface IAutService
    {
        Task<GeneralResponseDto> RegisterAsync(RegisterDto register);
        Task ConfirmEmail(string token, string userId);
        Task<Tokens> RefreshToken(Tokens tokenModel);

		Task<TokenResponseDto> LoginAsync(LoginDto login);
        Task<GeneralResponseDto> ForgotPasswordAsync(ForgotPasswordDto forgotPassword);
        Task ResetPasswordAsync(string token, string email, ResetPasswordDto resetPassword);


    }
}
