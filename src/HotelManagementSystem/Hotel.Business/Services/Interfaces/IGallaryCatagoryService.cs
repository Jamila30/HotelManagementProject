namespace Hotel.Business.Services.Interfaces
{
	public interface IGallaryCatagoryService
	{
		Task<List<GallaryCatagoryDto>> GetAllAsync();
		Task<List<GallaryCatagoryDto>> GetByCondition(Expression<Func<GallaryCatagory, bool>> expression);
		Task<GallaryCatagoryDto?> GetByIdAsync(int id);
		Task Create(CreateCatagoryDto entity);
		Task UpdateAsync(int id, UpdateCatagoryDto entity);
		Task Delete(int id);

	}
}
