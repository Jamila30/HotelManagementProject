namespace Hotel.Business.DTOs.ServiceImageDTOs
{
	public class CreateServiceImageDto:IDto
	{
		public IFormFile? Image { get; set; }
	}
}
