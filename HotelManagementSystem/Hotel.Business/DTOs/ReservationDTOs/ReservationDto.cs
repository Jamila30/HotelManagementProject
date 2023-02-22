namespace Hotel.Business.DTOs.ReservationDTOs
{
	public class ReservationDto:IDto
	{
		public int Id { get; set; }
		public int FlatId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int Adults { get; set; }
		public int Children { get; set; }
		public string? PhoneNumber { get; set; }
		public string? GuestName { get; set; }
		public string? UserId { get; set; }
		public float Price { get; set; }
	}
}
