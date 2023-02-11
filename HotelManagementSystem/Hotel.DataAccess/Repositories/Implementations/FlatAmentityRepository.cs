namespace Hotel.DataAccess.Repositories.Implementations
{
	public class FlatAmentityRepository : Repository<FlatAmentity>, IFlatAmentityRepository
	{
		public FlatAmentityRepository(AppDbContext context) : base(context)
		{
		}
	}
}
