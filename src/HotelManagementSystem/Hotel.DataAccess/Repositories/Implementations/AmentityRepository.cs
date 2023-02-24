namespace Hotel.DataAccess.Repositories.Implementations
{
	public class AmentityRepository : Repository<Amentity>, IAmentityRepository
	{
		public AmentityRepository(AppDbContext context) : base(context)
		{
		}
	}
}
