using Hotel.Business.DTOs.ReservationDTOs;

namespace Hotel.Business.Services.Interfaces
{
	public interface IReservationService
	{
		Task<List<ReservationDto>> GetAllAsync();
		Task<List<ReservationDto>> GetByCondition(Expression<Func<Reservation, bool>> expression);
		Task<ReservationDto?> GetByIdAsync(int id);
		Task CreateRezerv(StabilPropertirsDto stabil, List<CreateReservationDto> entities);
		Task UpdateAsync(int id, UpdateReservationDto entity);
		Task Delete(int id);
	}
}
