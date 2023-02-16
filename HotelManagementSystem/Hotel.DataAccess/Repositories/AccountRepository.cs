using Hotel.DataAccess.Repositories.Implementations;

namespace Hotel.DataAccess.Repositories
{
	public class AccountRepository : Repository<AppUser>, IAccountRepository
	{
		public AccountRepository(AppDbContext context) : base(context)
		{
		}
	}
}
