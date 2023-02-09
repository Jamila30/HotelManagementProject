namespace Hotel.Business.DTOs.GallaryImageDTOs
{
	public class CreateGallaryImageDto:IDto
	{
	
		public IFormFile? Image { get; set; }
		public int GallaryCatagoryId { get; set; }
	}
}
