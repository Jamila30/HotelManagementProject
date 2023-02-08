

namespace Hotel.DataAccess.Repositories.Implementations
{
	public class ServiceImageRepository : Repository<ServiceImage>, IServiceImageRepository
	{
		public ServiceImageRepository(AppDbContext context) : base(context)
		{
		}
	}
}
