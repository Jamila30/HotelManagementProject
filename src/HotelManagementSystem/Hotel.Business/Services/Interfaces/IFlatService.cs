namespace Hotel.Business.Services.Interfaces
{
	public interface IFlatService
	{
		Task<List<FlatDto>> GetAllAsync();
		Task<List<FlatDto>> GetByCondition(Expression<Func<Flat, bool>> expression);
		Task<FlatDto?> GetByIdAsync(int id);
		Task AddAmentityToFlat(int amentityId, int flatId);
		Task DeleteAmentityFromFlat(int amentityId, int flatId);
		Task UpdateAmentityOfFlat(int amentityId, int newAmentityId, int flatId);
		Task Create(CreateFlatDto entity);
		Task UpdateAsync(int id, UpdateFlatDto entity);
		Task Delete(int id);
	}
}
