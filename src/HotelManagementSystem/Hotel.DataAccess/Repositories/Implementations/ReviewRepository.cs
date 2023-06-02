namespace Hotel.DataAccess.Repositories.Implementations
{
	public class ReviewRepository : Repository<Review>, IReviewRepository
	{
		public ReviewRepository(AppDbContext context) : base(context)
		{
		}
	}
}
