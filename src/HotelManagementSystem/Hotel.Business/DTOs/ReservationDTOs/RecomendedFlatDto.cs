namespace Hotel.Business.DTOs.ReservationDTOs
{
	public class RecomendedFlatDto:IDto
	{
		public int CatagoryId { get; set; }
		public int FlatId { get; set; }
		public int BedCount { get; set; }
		public float Price { get; set; }
		
		
	}
}
