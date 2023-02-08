namespace Hotel.Business.Services.Interfaces
{
	public interface IWhyUsService
	{
		Task<List<WhyUsDto>> GetAllAsync();
		Task<List<WhyUsDto>> GetByCondition(Expression<Func<WhyUs, bool>> expression);
		Task<WhyUsDto?> GetByIdAsync(int id);
		Task Create(CreateWhyUsDto entity);
		Task UpdateAsync(int id, UpdateWhyUsDto entity);
		Task Delete(int id);
	}
}
