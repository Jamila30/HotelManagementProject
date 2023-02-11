namespace Hotel.Business.Services.Interfaces
{
	public interface IFlatAmentityService
	{
		Task<List<FlatAmentityDto>> GetAllAsync();
		Task<List<FlatAmentityDto>> GetByCondition(Expression<Func<FlatAmentity, bool>> expression);
		Task<FlatAmentityDto?> GetByIdAsync(int id);
		Task Create(CreateFlatAmentityDto entity);
		Task UpdateAsync(int id, UpdateFlatAmentityDto entity);
		Task Delete(int id);
	}
}
