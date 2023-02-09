namespace Hotel.Business.Services.Interfaces
{
	public interface IFlatService
	{
		Task<List<NearPlaceDto>> GetAllAsync();
		Task<List<NearPlaceDto>> GetByCondition(Expression<Func<NearPlace, bool>> expression);
		Task<NearPlaceDto?> GetByIdAsync(int id);
		Task Create(CreateNearPlaceDto entity);
		Task UpdateAsync(int id, UpdateNearPlaceDto entity);
		Task Delete(int id);
	}
}
