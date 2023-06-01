using entity = Hotel.Core.Entities;
namespace Hotel.Business.Services.Interfaces
{
	public interface IReviewService
	{
		Task<List<ReviewDto>> GetAllAsync();
		Task<List<ReviewDto>> GetByCondition(Expression<Func<entity.Review, bool>> expression);
		Task<ReviewDto?> GetByIdAsync(int id);
		Task Create(CreateReviewDto entity);
		Task Update(int id, UpdateReviewDto entity);
		Task Delete(int id);
	}
}
