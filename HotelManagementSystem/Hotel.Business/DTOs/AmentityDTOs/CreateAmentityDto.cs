namespace Hotel.Business.DTOs.AmentityDTOs
{
	public class CreateAmentityDto:IDto
	{
		public IFormFile? Image { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
	}
}
