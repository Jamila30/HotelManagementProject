namespace Hotel.Business.DTOs.ReservationDTOs
{
	public class WholeReservationDto:IDto
	{
		public DateDto? dateDto { get; set; }
		public string? UserId { get; set; }
		
	}
}
