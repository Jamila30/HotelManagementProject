namespace Hotel.Business.DTOs.ReservationDTOs
{
	public class StabilPropertirsDto:IDto
	{
		public DateTime CheckInDate { get; set; }
		public DateTime CheckOutDate { get; set; }
		public string? UserId { get; set; }
	}
}
