using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hotel.Business.Helper_Services.Interfaces
{
	public interface ITokenCreatorService
	{
		Task<TokenResponseDto> CreateTokenForUser(AppUser user, int minute);
		string GenerateRefreshToken();
		ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
	}
}
