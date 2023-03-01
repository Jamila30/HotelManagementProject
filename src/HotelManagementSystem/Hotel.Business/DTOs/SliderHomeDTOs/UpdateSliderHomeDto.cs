namespace Hotel.Business.DTOs.SliderHomeDTOs
{
	public class UpdateSliderHomeDto : IDto
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public IFormFile? Image { get; set; }
		
	}
}
