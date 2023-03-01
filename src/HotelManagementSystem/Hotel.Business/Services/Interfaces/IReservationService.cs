namespace Hotel.Business.Services.Interfaces
{
	public interface IReservationService
	{
		Task<List<ReservationDto>> GetAllAsync();
		Task<List<ReservationDto>> GetByCondition(Expression<Func<Reservation, bool>> expression);
		Task<ReservationDto?> GetByIdAsync(int id);
		Task CreateRezerv(string UserId, DateTime CheckInDate, DateTime CheckOutDate, List<CreateReservationDto>? entities);
		Task UpdateAsync(int id, UpdateReservationDto entity);
		Task Delete(int id);
		Task CancelReservation(int reservId);
		Task<bool> IsReserve(int flatId, DateDto date);
		Task<List<AvailableFlatsDto>> AvailableFlatsForReserve(DateDto date);
		Task<List<RecomendedFlatDto>> RecomendedFlats(DateDto dateDto, int adults = 1, int children = 0);
		Task<float> GetTotalPrice(List<StabilPropertirsDto> reservId);
		Task FinishEndedReservations();

	}
}
