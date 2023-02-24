using Hotel.Business.Interfaces;

namespace Hotel.Business.DTOs.WhyUsDTOs
{
	public class WhyUsDto : IDto
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
	}
}
