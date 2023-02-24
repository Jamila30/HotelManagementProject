namespace Hotel.Business.DTOs.ServiceImageDTOs
{
	public class ServiceImageDto:IDto
	{
		public int Id { get; set; }
		public string? Image { get; set; }
		public int ServiceOfferId { get; set; }
	}
}
