
namespace Hotel.Business.DTOs.NearPlaceDTOs
{
	public class UpdateNearPlaceDto:IDto
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public IFormFile? Image { get; set; }
	}
}
