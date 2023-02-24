namespace Hotel.Business.DTOs.ServiceImageDTOs
{
	public class UpdateServiceImageDto:IDto
	{
		public int Id { get; set; }
		public IFormFile? Image { get; set; }

		public int ServiceOfferId { get; set; }
	}
}
