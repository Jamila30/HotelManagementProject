using Hotel.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Business.DTOs.WhyUsDTOs
{
	public class UpdateWhyUsDto:IDto
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public IFormFile? Image { get; set; }
	}
}
