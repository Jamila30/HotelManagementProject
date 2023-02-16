namespace Hotel.Business.Helper_Services.Interfaces
{
	public interface ITokenCreatorService
	{
		Task<TokenResponseDto> CreateTokenForUser(AppUser user, int minute);
	}
}
