using Hotel.Business.DTOs.AmentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Business.Services.Interfaces
{
	public interface IAmentityService
	{
		Task<List<AmentityDto>> GetAllAsync();
		Task<List<AmentityDto>> GetByCondition(Expression<Func<Amentity, bool>> expression);
		Task<AmentityDto?> GetByIdAsync(int id);
		Task Create(CreateAmentityDto entity);
		Task UpdateAsync(int id, UpdateAmentityDto entity);
		Task Delete(int id);
	}
}
