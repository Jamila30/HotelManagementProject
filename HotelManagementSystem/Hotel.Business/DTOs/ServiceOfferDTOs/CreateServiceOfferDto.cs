namespace Hotel.Business.DTOs.ServiceOfferDTOs
{
	public class CreateServiceOfferDto:IDto
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public float? Price { get; set; }
		public bool IsFree { get; set; }
	}
}
