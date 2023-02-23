namespace Hotel.Business.DTOs.ReservationDTOs
{
	public class AvailableFlatsDto:IDto
	{
		public int Id { get; set; }
		public int CatagoryId { get; set; }

		public int FlatId { get; set; }

	}
}
