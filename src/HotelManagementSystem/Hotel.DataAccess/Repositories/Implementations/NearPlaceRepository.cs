
namespace Hotel.DataAccess.Repositories.Implementations
{
	public class NearPlaceRepository : Repository<NearPlace>, INearPlaceRepository
	{
		public NearPlaceRepository(AppDbContext context) : base(context)
		{
		}
	}
}
