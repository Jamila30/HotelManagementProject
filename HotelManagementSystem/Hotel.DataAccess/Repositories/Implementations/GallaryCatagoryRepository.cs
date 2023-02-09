namespace Hotel.DataAccess.Repositories.Implementations
{
	public class GallaryCatagoryRepository : Repository<GallaryCatagory>, IGallaryCatagoryRepository
	{
		public GallaryCatagoryRepository(AppDbContext context) : base(context)
		{
		}
	}
}
