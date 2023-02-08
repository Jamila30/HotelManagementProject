

namespace Hotel.Business.DTOs.NearPlaceDTOs
{
	public class CreateNearPlaceDto:IDto
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public IFormFile? Image { get; set; }
	}
}
