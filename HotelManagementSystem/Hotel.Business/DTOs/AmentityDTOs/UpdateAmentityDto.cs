namespace Hotel.Business.DTOs.AmentityDTOs
{
	public class UpdateAmentityDto:IDto
	{
		public int Id { get; set; }
		public IFormFile? Image { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
	}
}
