namespace Hotel.Business.Services.Interfaces
{
	public interface ISliderHomeService
	{
		Task<List<SliderHomeDto>> GetAllAsync();
		Task<List<SliderHomeDto>> GetByCondition(Expression<Func<SliderHome, bool>> expression);
		Task<SliderHomeDto?> GetByIdAsync(int id);
		Task Create(CreateSliderHomeDto entity);
		Task UpdateAsync(int id,UpdateSliderHomeDto entity);
		Task Delete(int id);

	}
}
