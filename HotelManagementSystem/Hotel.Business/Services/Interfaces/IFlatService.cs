namespace Hotel.Business.Services.Interfaces
{
	public interface IFlatService
	{
		Task<List<FlatDto>> GetAllAsync();
		Task<List<FlatDto>> GetByCondition(Expression<Func<Flat, bool>> expression);
		Task<FlatDto?> GetByIdAsync(int id);
		Task Create(CreateFlatDto entity);
		Task UpdateAsync(int id, UpdateFlatDto entity);
		Task Delete(int id);
	}
}
