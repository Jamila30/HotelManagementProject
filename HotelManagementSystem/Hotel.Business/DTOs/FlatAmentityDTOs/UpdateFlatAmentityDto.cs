namespace Hotel.Business.DTOs.FlatAmentityDTOs
{
	public class UpdateFlatAmentityDto:IDto
	{
		public int Id { get; set; }
		public int FlatId { get; set; }
		public int AmentityId { get; set; }
	}
}
