using Hotel.Core.Entities;

namespace Hotel.Business.Services.Implementations
{
	public class FlatAmentityService : IFlatAmentityService
	{
		private readonly IFlatAmentityRepository _repository;
		private readonly IMapper _mapper;
		public FlatAmentityService(IFlatAmentityRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<List<FlatAmentityDto>> GetAllAsync()
		{
			var list = await _repository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<FlatAmentityDto>>(list);
			return listDto;
		}

		public async Task<List<FlatAmentityDto>> GetByCondition(Expression<Func<FlatAmentity, bool>> expression)
		{
			var list = await _repository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<FlatAmentityDto>>(list);
			return listDto;
		}

		public async Task<FlatAmentityDto?> GetByIdAsync(int id)
		{
			var amentity = await _repository.GetByIdAsync(id);
			var amentityDto = _mapper.Map<FlatAmentityDto>(amentity);
			return amentityDto;
		}
		public async Task Create(CreateFlatAmentityDto entity)
		{
			var amentityId = _repository.GetAll().FirstOrDefault(x => x.AmentityId == entity.AmentityId);
			if (amentityId is null) throw new NotFoundException("there is not amentity with this id");
			var flatId = _repository.GetAll().FirstOrDefault(x => x.FlatId == entity.FlatId);
			if (flatId is null) throw new NotFoundException("there is not flat with this id");
			FlatAmentity flatAmentity = new();
			flatAmentity.FlatId = entity.FlatId;
			flatAmentity.AmentityId = entity.AmentityId;
			await _repository.Create(flatAmentity);
			await _repository.SaveChanges();
		}
		public async Task UpdateAsync(int id, UpdateFlatAmentityDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id did match another");
			var amentity = _repository.GetAll().FirstOrDefault(x => x.Id == entity.AmentityId);
			if (amentity is null) throw new NotFoundException("there is no amentity to update");
			var flatId = _repository.GetAll().FirstOrDefault(x => x.FlatId == entity.FlatId);
			if (flatId is null) throw new NotFoundException("there is not flat with this id");
			var flatAmentityDto = _repository.GetByCondition(x => x.Id == id).First();
			if (flatAmentityDto is null) throw new NotFoundException("there is no data with this id");
			var result =_mapper.Map<FlatAmentity>(flatAmentityDto);
			_repository.Update(result);
			await _repository.SaveChanges();
		}
		public async Task Delete(int id)
		{
			var flatAmentity = await _repository.GetByIdAsync(id);
			if (flatAmentity is null) throw new NotFoundException("there is no amentity to delete");
			_repository.Delete(flatAmentity);
			await _repository.SaveChanges();
		}
	}
}
