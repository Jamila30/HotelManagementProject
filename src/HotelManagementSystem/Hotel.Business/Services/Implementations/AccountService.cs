using Hotel.Business.Exceptions;
using Microsoft.AspNetCore.Identity;

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
			var account = await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == accountId);
			if (account is null) throw new NotFoundException("there is no account for this id");
			var user = _mapper.Map<AppUserDto>(account);
			return user;
		}
		public async Task CreateAccount(CreateAccountDto createAccount)
		{
			var account = _mapper.Map<AppUser>(createAccount);
			account.EmailConfirmed = true;
			var identityResult = await _userManager.CreateAsync(account, createAccount.Password);
			string errors = string.Empty;
			int count = 1;
			if (!identityResult.Succeeded)
			{
				foreach (var error in identityResult.Errors)
				{
					errors += count+"." + error.Description+"\n";
					count++;
				}
				throw new BadRequestException(errors.Trim());
			}
			await _userManager.AddToRoleAsync(account, Roles.Member.ToString());

		}
		public async Task UpdateAccount(string userId, UpdateUserDto createAccount)
		{
			if (userId != createAccount.userId) throw new IncorrectIdException("id didnt overlap");
			var user = await _userManager.FindByIdAsync(userId);
			if (user is null) throw new NotFoundException("user didnt find");
			user.Fullname = createAccount.FullName;
			user.PhoneNumber = createAccount.PhoneNumber;
			var existUsername=await _userManager.FindByNameAsync(createAccount.UserName);
			if(existUsername !=null) throw new BadRequestException("This username is taken"); 
			user.UserName = createAccount.UserName;
			_repository.Update(user);
			await _repository.SaveChanges();
		}
		public async Task<bool> BlockAccount(BlockAccountDto blockAccount)
		{
			var user = await _userManager.FindByEmailAsync(blockAccount.Email);
			if (user is null) throw new NotFoundException("There is no account with this email");

			var lockUser = await _userManager.SetLockoutEnabledAsync(user, true);
			var errors = string.Empty;
			int count = 1;
			if (!lockUser.Succeeded)
			{
				foreach (var error in lockUser.Errors)
				{
					errors += count + "." + error.Description + "\n";
					count++;
				}
				throw new BadRequestException(errors.Trim());
			}
			var lockDate = await _userManager.SetLockoutEndDateAsync(user, blockAccount.EndDate);
			if (!lockDate.Succeeded)
			{
				foreach (var error in lockUser.Errors)
				{
					errors += count + "." + error.Description + "\n";
					count++;
				}
				throw new BadRequestException(errors.Trim());
			}

			return true;
		}
		public async Task<bool> UnBlockAccount(JustEmailDto justEmail)
		{
			var user = await _userManager.FindByEmailAsync(justEmail.Email);
			if (user is null) throw new NotFoundException("There is not account with this email");

			var errors = string.Empty;
			int count = 1;
			var DATE = DateTime.Now - TimeSpan.FromMinutes(1);
			var lockDate = await _userManager.SetLockoutEndDateAsync(user,null );
			if (!lockDate.Succeeded)
			{
				foreach (var error in lockDate.Errors)
				{
					errors += count + "." + error.Description + "\n";
					count++;
				}
				throw new BadRequestException(errors.Trim());
			}

			var lockUser = await _userManager.SetLockoutEnabledAsync(user, false);
			if (!lockUser.Succeeded)
			{
				foreach (var error in lockUser.Errors)
				{
					errors += count + "." + error.Description + "\n";
					count++;
				}
				throw new BadRequestException(errors.Trim());
			}
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
			var result = await _userManager.IsInRoleAsync(user, updateUser.NewRoleName);
			if (result) throw new BadRequestException("new role name exists for this user");
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
			var result = await _userManager.IsInRoleAsync(user, deleteRole.RoleName);
			if (!result) throw new BadRequestException("role name doesnt exists for this user");

			var roles = await _userManager.GetRolesAsync(user);
			if (roles.Count() <= 1) throw new BadRequestException("role cant deleted because another role doesnt exist");
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
