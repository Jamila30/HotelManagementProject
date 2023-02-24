namespace Hotel.Business.DTOs.ReservationDTOs
{
	public class DateDto:IDto
	{
		public DateTime CheckInDate { get; set; }
		public DateTime CheckOutDate { get; set; }
	}
}
