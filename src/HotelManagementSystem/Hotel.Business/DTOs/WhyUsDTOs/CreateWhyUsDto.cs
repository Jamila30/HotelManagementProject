using Hotel.Business.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Hotel.Business.DTOs.WhyUsDTOs
{
	public class CreateWhyUsDto : IDto
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public IFormFile? Image { get; set; }
	}
}
