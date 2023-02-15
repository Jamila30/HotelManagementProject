namespace Hotel.DataAccess.Contexts
{
	public class AppDbContextInitializer
	{
		public readonly UserManager<AppUser> _userManager;
		public readonly RoleManager<IdentityRole> _roleManager;
		public readonly IConfiguration _configuration;
		public readonly AppDbContext _context;

		public AppDbContextInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, AppDbContext context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
			_context = context;
		}

		public async Task InitializeAsync()
		{
			await _context.Database.MigrateAsync();
		}
		public async Task RoleSeedAsync()
		{
			foreach (var role in Enum.GetValues(typeof(Roles)))
			{

				if (!await _roleManager.RoleExistsAsync(role.ToString()))
				{
					await _roleManager.CreateAsync(new IdentityRole(role.ToString()));
				}
			}
		}
		public async Task UserSeedAsync()
		{
			AppUser admin = new()
			{
				//UserName = _configuration.GetConnectionString("Username"),
				UserName = _configuration["AdminSettings:Username"],
				Email = _configuration["AdminSettings:Email"],
				EmailConfirmed = true
			};

			await _userManager.CreateAsync(admin, _configuration["AdminSettings:Password"]);
			await _userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
		}

	}
}
