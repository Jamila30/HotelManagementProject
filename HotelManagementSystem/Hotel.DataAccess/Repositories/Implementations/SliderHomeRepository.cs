using Hotel.Core.Entities;
using Hotel.DataAccess.Contexts;
using Hotel.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Repositories.Implementations
{
	public class SliderHomeRepository : Repository<SliderHome>, ISliderHomeRepository
	{
		public SliderHomeRepository(AppDbContext context) : base(context)
		{
		}
	}
}
