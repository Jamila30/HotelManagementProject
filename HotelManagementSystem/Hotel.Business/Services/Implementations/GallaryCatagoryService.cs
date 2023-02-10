namespace Hotel.Business.Services.Implementations
{
	public class GallaryCatagoryService : IGallaryCatagoryService
	{
		private readonly IGallaryCatagoryRepository _repository;
		private readonly IMapper _mapper;
		public GallaryCatagoryService(IGallaryCatagoryRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}


		public async Task<List<GallaryCatagoryDto>> GetAllAsync()
		{
			var listAll = _repository.GetAll().ToList();
			var listDto = _mapper.Map<List<GallaryCatagoryDto>>(listAll);
			return listDto;
		}

		public async Task<List<GallaryCatagoryDto>> GetByCondition(Expression<Func<GallaryCatagory, bool>> expression)
		{
			var listAll = _repository.GetAll().Where(expression).ToList();
			var listDto = _mapper.Map<List<GallaryCatagoryDto>>(listAll);
			return listDto;
		}

		public async Task<GallaryCatagoryDto?> GetByIdAsync(int id)
		{
			var catagory = await _repository.GetByIdAsync(id);
			if (catagory is null) throw new NotFoundException("Element not found");
			var catagoryDto = _mapper.Map<GallaryCatagoryDto>(catagory);
			return catagoryDto;
		}

		public async Task Create(CreateCatagoryDto entity)
		{
			GallaryCatagory gallaryCatagory = new()
			{
				Name = entity.Name,
			};
			var sameNameList = _repository.GetByCondition(x => x.Name == entity.Name).ToList();
			if (sameNameList.Count >= 1) throw new RepeatedSameCatagoryNameException("This catagory name exist");
			await _repository.Create(gallaryCatagory);
			await _repository.SaveChanges();
		}

		public async Task UpdateAsync(int id, UpdateCatagoryDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id didnt match another");
			var catagory = await _repository.GetByIdAsync(id);
			if (catagory is null) throw new NotFoundException("There is no catagory for update with this id");
			catagory.Name = entity.Name;
			var sameNameList = _repository.GetByCondition(x => x.Name == entity.Name).ToList();
			if (sameNameList.Count >= 1) throw new RepeatedSameCatagoryNameException("This catagory name exist");

			_repository.Update(catagory);
			await _repository.SaveChanges();

		}
		public async Task Delete(int id)
		{
			var catagory = await _repository.GetByIdAsync(id);
			if (catagory is null) throw new NotFoundException("There is no catagory for update with this id");

			_repository.Delete(catagory);
			await _repository.SaveChanges();
		}
	}
}
