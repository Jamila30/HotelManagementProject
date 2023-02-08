using Hotel.Business.DTOs.ServiceImageDTOs;
using Hotel.Business.DTOs.ServiceOfferDTOs;

namespace Hotel.Business.Services.Interfaces
{
	public interface IServiceImageService
	{
		Task<List<ServiceImageDto>> GetAllAsync();
		Task<List<ServiceImageDto>> GetByCondition(Expression<Func<ServiceImage, bool>> expression);
		Task<ServiceImageDto?> GetByIdAsync(int id);
		//Task UpdateServiceOfferId(int imageId, int serviceId)
		Task CreateImageForServiceId(int serviceId, CreateServiceImageDto entity);

		Task UpdateAsync(int id, UpdateServiceImageDto entity);
		Task Delete(int id);
	}
}
