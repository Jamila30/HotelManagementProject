

namespace Hotel.DataAccess.Repositories.Implementations
{
	public class ServiceOfferRepository : Repository<ServiceOffer>, IServiceOfferRepository
	{
		public ServiceOfferRepository(AppDbContext context) : base(context)
		{
		}
	}
}
