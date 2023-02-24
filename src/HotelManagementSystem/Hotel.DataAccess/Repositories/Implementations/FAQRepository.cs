namespace Hotel.DataAccess.Repositories.Implementations
{
	public class FAQRepository : Repository<FAQ>, IFAQRepository
	{
		public FAQRepository(AppDbContext context) : base(context)
		{
		}
	}
}
