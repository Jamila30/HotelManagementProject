namespace Hotel.Business.DTOs.ReservationDTOs
{
	public class CreateReservationDto:IDto
	{
		public int FlatId { get; set; }
		public int Adults { get; set; }
		public int Children { get; set; }

	}
}
