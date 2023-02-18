namespace Hotel.Business.Services.Implementations
{
	public class AccountService : IAccountService
	{
		private readonly IAccountRepository _repository;
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;
		private readonly AppDbContext _context;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountService(IAccountRepository repository, IMapper mapper, UserManager<AppUser> userManager, AppDbContext context, RoleManager<IdentityRole> roleManager)
		{
			_repository = repository;
			_mapper = mapper;
			_userManager = userManager;
			_context = context;
			_roleManager = roleManager;
		}
		public async Task<List<AppUserDto>> GetAllAccounts()
		{
			var list = await _repository.GetAll().ToListAsync();
			var userDto = _mapper.Map<List<AppUserDto>>(list);
			return userDto;
		}
		public async Task<AppUserDto> GetAccount(string accountId)
		{
			if (accountId is null) throw new BadRequestException("Invalid form of user id");
			var account = await _repository.GetAll().FirstOrDefaultAsync(x=>x.Id==accountId);
			if (account is null) throw new NotFoundException("there is no account for this id");
			var user = _mapper.Map<AppUserDto>(account);
			return user;
		}
		public async Task CreateAccount(CreateAccountDto createAccount)
		{
			var account = _mapper.Map<AppUser>(createAccount);
			var identityResult=await _userManager.CreateAsync(account);
			if (!identityResult.Succeeded) throw new BadRequestException("Account didnt created");
			await _userManager.AddToRoleAsync(account, Roles.Member.ToString());

		}
		public async Task<bool> BlockAccount(BlockAccountDto blockAccount)
		{
			var user = await _userManager.FindByEmailAsync(blockAccount.Email);
			if (user is null) throw new NotFoundException("There is no account with this email");

			var lockUser = await _userManager.SetLockoutEnabledAsync(user, false);
			if (!lockUser.Succeeded) throw new BadRequestException("problem happened during lock user");

			var lockDate = await _userManager.SetLockoutEndDateAsync(user, blockAccount.EndDate);
			if (!lockDate.Succeeded) throw new BadRequestException("problem happened during lock user until date");
			return true;
		}
		public async Task<bool> UnBlockAccount(JustEmailDto justEmail)
		{
			var user = await _userManager.FindByEmailAsync(justEmail.Email);
			if (user is null) throw new NotFoundException("There is not account with this email");

			var lockUser = await _userManager.SetLockoutEnabledAsync(user, true);
			if (!lockUser.Succeeded) throw new BadRequestException("problem happened during unlock user");
			DateTime date = DateTime.Now - TimeSpan.FromMinutes(1);
			var lockDate = await _userManager.SetLockoutEndDateAsync(user, DateTime.Now - TimeSpan.FromMinutes(1));
			if (!lockDate.Succeeded) throw new BadRequestException("user unsuccessfully unlocked");
			return true;
		}
		public async Task DeleteAccount(JustEmailDto justEmail)
		{
			var user = await _userManager.FindByEmailAsync(justEmail.Email);
			if (user is null) throw new NotFoundException("There is not account with this email");
			if (user.IsDeleted == true) throw new BadRequestException("this account doesn't exist,deleted");
			user.IsDeleted = true;
			await _repository.SaveChanges();
		}
		public async Task AddUserRole(UserRoleDto addRole)
		{
			var user = await _userManager.FindByEmailAsync(addRole.Email);
			if (user is null) throw new NotFoundException("There is not account with this email");
			var roleCheck = await _roleManager.RoleExistsAsync(addRole.RoleName);
			if (!roleCheck) throw new NotFoundException("role name doesn't exist");
			var result = await _userManager.IsInRoleAsync(user, addRole.RoleName);
			if (result) throw new BadRequestException("new role name exists for this user");
			var roles = await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				if (!role.Equals(addRole.RoleName))
				{

					await _userManager.AddToRoleAsync(user, addRole.RoleName);
				}
				else
				{
					throw new AlreadyExistException("this role already exist for this user");
				}
			}
		}
		public async Task UpdateUserRole(UpdateUserRolesDto updateUser)
		{
			var user = await _userManager.FindByEmailAsync(updateUser.Email);
			if (user is null) throw new NotFoundException("There is not account with this email");
			var oldCheck = await _roleManager.RoleExistsAsync(updateUser.OldRoleName);
			if (!oldCheck) throw new NotFoundException("old role name doesn't exist");
			var newCheck = await _roleManager.RoleExistsAsync(updateUser.NewRoleName);
			if (!newCheck) throw new NotFoundException("new role name doesn't exist");
			var result =await _userManager.IsInRoleAsync(user, updateUser.NewRoleName);
			if(result) throw new BadRequestException("new role name exists for this user");
			var roles = await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				if (role.Equals(updateUser.OldRoleName) && !role.Equals(updateUser.NewRoleName))
				{
					await _userManager.RemoveFromRoleAsync(user, updateUser.OldRoleName);
					await _userManager.AddToRoleAsync(user, updateUser.NewRoleName);
				}

			}
		}
		public async Task DeleteUserRole(UserRoleDto deleteRole)
		{
			var user = await _userManager.FindByEmailAsync(deleteRole.Email);
			if (user is null) throw new NotFoundException("There is not account with this email");
			var roleCheck = await _roleManager.RoleExistsAsync(deleteRole.RoleName);
			if (!roleCheck) throw new NotFoundException("role name doesn't exist");
			var result = await _userManager.IsInRoleAsync(user,deleteRole.RoleName);
			if (!result) throw new BadRequestException("role name doesnt exists for this user");

			var roles = await _userManager.GetRolesAsync(user);
			bool isExist = false;
			foreach (var role in roles)
			{
				if (role.Equals(deleteRole.RoleName))
				{
					await _userManager.RemoveFromRoleAsync(user, deleteRole.RoleName);
					isExist = true;
				}

			}
			if (!isExist) throw new AlreadyExistException("this user has no such role");
		}

	}
}
