using Hotel.Business.DTOs.SliderHomeDTOs;
using Hotel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Business.Services.Interfaces
{
	public interface ISliderHomeService
	{
		Task<List<SliderHomeDto>> GetAllAsync();
		Task<List<SliderHomeDto>> GetByCondition(Expression<Func<SliderHome, bool>> expression);
		Task<SliderHomeDto?> GetByIdAsync(int id);
		Task Create(CreateSliderHomeDto entity);
		void Upate(SliderHome entity);
		void Delete(SliderHome entity);

	}
}
