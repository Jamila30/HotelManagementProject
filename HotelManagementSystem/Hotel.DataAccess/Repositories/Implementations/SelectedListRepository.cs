namespace Hotel.DataAccess.Repositories.Implementations
{
	public class SelectedListRepository : Repository<SelectedList>, ISelectedListRepository
	{
		public SelectedListRepository(AppDbContext context) : base(context)
		{
		}
	}
}
