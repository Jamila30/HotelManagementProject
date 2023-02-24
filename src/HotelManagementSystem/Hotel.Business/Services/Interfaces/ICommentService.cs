namespace Hotel.Business.Services.Interfaces
{
	public interface ICommentService
	{
		Task<List<CommentDto>> GetAllAsync();
		Task<List<CommentDto>> GetByCondition(Expression<Func<Comment, bool>> expression);
		Task<CommentDto?> GetByIdAsync(int id);
		Task Create(CreateCommentDto entity);
		Task UpdateAsync(int id, UpdateCommentDto entity);
		Task Delete(int id);
	}
}
