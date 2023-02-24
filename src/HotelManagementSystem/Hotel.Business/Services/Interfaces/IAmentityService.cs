namespace Hotel.Business.Services.Interfaces
{
	public interface IAmentityService
	{
		Task<List<AmentityDto>> GetAllAsync();
		Task<List<AmentityDto>> GetByCondition(Expression<Func<Amentity, bool>> expression);
		Task<List<AmentityDto>> GetAllAmentitiesByFlatId(int flatId);
		Task<AmentityDto?> GetByIdAsync(int id);
		Task Create(CreateAmentityDto entity);
		Task UpdateAsync(int id, UpdateAmentityDto entity);
		Task Delete(int id);
	}
}
