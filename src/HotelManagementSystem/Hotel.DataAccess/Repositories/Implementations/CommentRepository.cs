namespace Hotel.DataAccess.Repositories.Implementations
{
	public class CommentRepository : Repository<Comment>, ICommentRepository
	{
		public CommentRepository(AppDbContext context) : base(context)
		{
		}
	}
}
