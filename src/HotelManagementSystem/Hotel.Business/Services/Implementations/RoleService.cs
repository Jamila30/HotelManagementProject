using Hotel.Business.DTOs.RoleManageDTOs;

namespace Hotel.Business.Services.Implementations
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly AppDbContext _context;
		private HashSet<Roles> myRoles = new HashSet<Roles>();

		public RoleService(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, AppDbContext context)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_context = context;
		}

		public async Task<List<RoleInfoDto>> GetAllRoles()
		{
			List<RoleInfoDto> roles = new List<RoleInfoDto>();
			foreach (var item in _context.Roles)
			{
				var roleId = await _roleManager.GetRoleIdAsync(item);
				var roleName = await _roleManager.GetRoleNameAsync(item);
				roles.Add(new RoleInfoDto() { RoleId = roleId, RoleName = roleName });
			}
			return roles;
		}

		public async Task<List<RoleInfoDto>> GetRoles(string userId)
		{
			List<RoleInfoDto> roles = new List<RoleInfoDto>();
			if (userId is null) throw new BadRequestException("Enter valid user id");
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null) throw new NotFoundException("user with this id didn't find");
			var check = false;
			foreach (var role in _context.Roles)
			{
				var roleString = role.ToString();
				if (roleString != null)
				{
					check = await _userManager.IsInRoleAsync(user, roleString);
					if (check)
					{
						var roleId = await _roleManager.GetRoleIdAsync(role);
						var roleName = await _roleManager.GetRoleNameAsync(role);
						roles.Add(new RoleInfoDto() { RoleId = roleId, RoleName = roleName });
					}
				}
			}
			return roles;
		}

		public async Task CreateRole(string roleName)
		{
			var existenceCheck = await _roleManager.RoleExistsAsync(roleName);
			if (existenceCheck) throw new NotFoundException("role name already exists ");
		//	Helper.Roles.Add(roleName);
		}
		public async Task UpdateRole(UpdateRoleDto updateRole)
		{
			var oldCheck = await _roleManager.RoleExistsAsync(updateRole.OldRoleName);
			if (!oldCheck) throw new NotFoundException("old role name doesn't exist");
			var newCheck = await _roleManager.RoleExistsAsync(updateRole.NewRoleName);
			if (newCheck) throw new NotFoundException("new role name already exist");
			if (updateRole.OldRoleName != null && updateRole.NewRoleName != null)
			{
				//Helper.Roles.Remove(updateRole.OldRoleName);
				//Helper.Roles.Add(updateRole.NewRoleName);
			}
			
			

		}
		public Task DeleteRole(string roleName)
		{
			throw new NotImplementedException();
		}
	}
}
