namespace Hotel.Business.Services.Interfaces
{
	public interface IAccountService
	{
		Task<List<AppUserDto>> GetAllAccounts();
		Task<AppUserDto> GetAccount(string accountId);
		Task CreateAccount(CreateAccountDto createAccount);
		Task<bool> BlockAccount(BlockAccountDto blockAccount);
		Task<bool> UnBlockAccount(JustEmailDto justEmail);
		Task DeleteAccount(JustEmailDto justEmail);
		Task AddUserRole(UserRoleDto addRole);
		Task UpdateUserRole(UpdateUserRolesDto updateUser);
		Task DeleteUserRole(UserRoleDto deleteRole);

	}
}
