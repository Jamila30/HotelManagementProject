using Hotel.Business.DTOs.RoomCatagoryDTOs;

namespace Hotel.Business.Services.Interfaces
{
	public interface IRoomCatagoryService
	{

		Task<List<RoomCatagoryDto>> GetAllAsync();
		Task<List<RoomCatagoryDto>> GetByCondition(Expression<Func<RoomCatagory, bool>> expression);
		Task<RoomCatagoryDto?> GetByIdAsync(int id);
		Task Create(CreateRoomCatagoryDto entity);
		Task UpdateAsync(int id, UpdateRoomCatagoryDto entity);
		Task Delete(int id);
	}
}
