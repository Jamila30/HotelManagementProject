namespace Hotel.DataAccess.Repositories.Implementations
{
	public class SentQuestionRepository : Repository<SentQuestion>, ISentQuestionRepository
	{
		public SentQuestionRepository(AppDbContext context) : base(context)
		{
		}
	}
}
