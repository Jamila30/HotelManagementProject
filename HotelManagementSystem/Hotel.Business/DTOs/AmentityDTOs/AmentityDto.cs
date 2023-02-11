namespace Hotel.Business.DTOs.AmentityDTOs
{
	public class AmentityDto:IDto
	{
		public int Id { get; set; }
		public string? Image { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
	}
}
