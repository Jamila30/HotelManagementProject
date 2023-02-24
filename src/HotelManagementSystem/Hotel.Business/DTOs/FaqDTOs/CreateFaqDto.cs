using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Business.DTOs.FaqDTOs
{
	public class CreateFaqDto:IDto
	{
		public string? Question { get; set; }
		public string? Answer { get; set; }

	}
}
