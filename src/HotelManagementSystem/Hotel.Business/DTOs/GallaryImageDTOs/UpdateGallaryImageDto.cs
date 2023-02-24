namespace Hotel.Business.DTOs.GallaryImageDTOs
{
	public class UpdateGallaryImageDto:IDto
	{
		public int Id { get; set; }
		public IFormFile? Image { get; set; }
		public int GallaryCatagoryId { get; set; }
	}
}
