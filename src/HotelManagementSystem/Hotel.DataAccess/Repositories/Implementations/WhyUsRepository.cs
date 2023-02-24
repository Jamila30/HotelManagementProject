

namespace Hotel.DataAccess.Repositories.Implementations
{
	public class WhyUsRepository : Repository<WhyUs>, IWhyUsRepository
	{
		public WhyUsRepository(AppDbContext context) : base(context)
		{
		}
	}
}
