using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Business.DTOs.SliderHomeDTOs
{
	public class UpdateSliderHomeDto
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public IFormFile? Image { get; set; }
		public string? ImagePath { get; set; }
	}
}
