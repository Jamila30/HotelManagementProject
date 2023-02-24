

namespace Hotel.Business.DTOs.SliderHomeDTOs
{
	public class CreateSliderHomeDto:IDto
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public IFormFile? Image { get; set; }
	}
}
