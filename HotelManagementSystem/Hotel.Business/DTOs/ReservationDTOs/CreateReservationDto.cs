namespace Hotel.Business.DTOs.ReservationDTOs
{
	public class CreateReservationDto:IDto
	{
		public int FlatId { get; set; }
		public DateTime CheckInDate { get; set; }
		public DateTime CheckOutDate { get; set; }
		public int Adults { get; set; }
		public int Children { get; set; }
		public string? UserId { get; set; }
		public float Price { get; set; }
	}
}
