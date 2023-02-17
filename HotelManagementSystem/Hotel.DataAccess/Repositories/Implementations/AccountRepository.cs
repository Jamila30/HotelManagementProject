namespace Hotel.DataAccess.Repositories.Implementations
{
    public class AccountRepository : Repository<AppUser>, IAccountRepository
    {
        public AccountRepository(AppDbContext context) : base(context)
        {
        }
    }
}
