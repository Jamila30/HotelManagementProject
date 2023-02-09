using Hotel.Business.DTOs.GallaryImageDTOs;

namespace Hotel.Business.Services.Interfaces
{
	public interface IGallaryImageService
	{
		Task<List<GallaryImageDto>> GetAllAsync();
		Task<List<GallaryImageDto>> GetByCondition(Expression<Func<GallaryImage, bool>> expression);
		Task<GallaryImageDto?> GetByIdAsync(int id);
		Task Create(CreateGallaryImageDto entity);
		Task UpdateAsync(int id, UpdateGallaryImageDto entity);
		Task Delete(int id);
	}
}
