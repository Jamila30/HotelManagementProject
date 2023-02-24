namespace Hotel.DataAccess.Repositories.Implementations
{
	public class GallaryImageRepository : Repository<GallaryImage>, IGallaryImageRepository
	{
		public GallaryImageRepository(AppDbContext context) : base(context)
		{
		}
	}
}
