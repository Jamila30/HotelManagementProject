using Hotel.Business.DTOs.RoomImageDTOs;

namespace Hotel.Business.Services.Interfaces
{
	public interface IRoomImageService
	{
		Task<List<RoomImageDto>> GetAllAsync();
		Task<List<RoomImageDto>> GetByCondition(Expression<Func<RoomImage, bool>> expression);
		Task<RoomImageDto?> GetByIdAsync(int id);
		Task Create(CreateRoomImageDto entity);
		Task UpdateAsync(int id, UpdateRoomImageDto entity);
		Task Delete(int id);
	}
}
