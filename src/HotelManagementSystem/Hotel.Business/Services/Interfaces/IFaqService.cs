namespace Hotel.Business.Services.Interfaces
{
	public interface IFaqService
	{
		Task<List<FaqDto>> GetAllAsync();
		Task<List<FaqDto>> GetByCondition(Expression<Func<FAQ, bool>> expression);
		Task<FaqDto?> GetByIdAsync(int id);
		Task Create(CreateFaqDto entity);
		Task UpdateAsync(int id, UpdateFaqDto entity);
		Task Delete(int id);
	}
}
