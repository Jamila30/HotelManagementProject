namespace Hotel.DataAccess.Repositories.Implementations
{
	public class FlatRepository : Repository<Flat>, IFlatRepository
	{
		public FlatRepository(AppDbContext context) : base(context)
		{
		}
	}
}
