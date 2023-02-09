namespace Hotel.Business.DTOs.GallaryImageDTOs
{
	public class GallaryImageDto:IDto
	{
		public int Id { get; set; }
		public string? Image { get; set; }
		public int GallaryCatagoryId { get; set; }
	}
}
