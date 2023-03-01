namespace Hotel.Business.Services.Implementations
{
	public class GallaryCatagoryService : IGallaryCatagoryService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public GallaryCatagoryService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}


		public async Task<List<GallaryCatagoryDto>> GetAllAsync()
		{
			var listAll = await _unitOfWork.gallaryCatagoryRepository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<GallaryCatagoryDto>>(listAll);
			return listDto;
		}

		public async Task<List<GallaryCatagoryDto>> GetByCondition(Expression<Func<GallaryCatagory, bool>> expression)
		{
			var listAll = await _unitOfWork.gallaryCatagoryRepository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<GallaryCatagoryDto>>(listAll);
			return listDto;
		}

		public async Task<GallaryCatagoryDto?> GetByIdAsync(int id)
		{
			var catagory = await _unitOfWork.gallaryCatagoryRepository.GetAll().Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);
			if (catagory is null) throw new NotFoundException("Element not found");
			var catagoryDto = _mapper.Map<GallaryCatagoryDto>(catagory);
			return catagoryDto;
		}

		public async Task Create(CreateCatagoryDto entity)
		{
			GallaryCatagory gallaryCatagory = new() { Name = entity.Name };
			var sameNameList = _unitOfWork.gallaryCatagoryRepository.GetByCondition(x => x.Name == entity.Name).ToList();
			if (sameNameList.Count >= 1) throw new RepeatedSameCatagoryNameException("This catagory name exist");
			await _unitOfWork.gallaryCatagoryRepository.Create(gallaryCatagory);
			await _unitOfWork.SaveAsync();
		}

		public async Task UpdateAsync(int id, UpdateCatagoryDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id didnt match another");
			var catagory = await _unitOfWork.gallaryCatagoryRepository.GetByIdAsync(id);
			if (catagory is null) throw new NotFoundException("There is no catagory for update with this id");
			catagory.Name = entity.Name;
			var sameNameList = _unitOfWork.gallaryCatagoryRepository.GetByCondition(x => x.Name == entity.Name).ToList();
			if (sameNameList.Count >= 1) throw new RepeatedSameCatagoryNameException("This catagory name exist");

			_unitOfWork.gallaryCatagoryRepository.Update(catagory);
			await _unitOfWork.SaveAsync();

		}
		public async Task Delete(int id)
		{
			var catagory = await _unitOfWork.gallaryCatagoryRepository.GetByIdAsync(id);
			if (catagory is null) throw new NotFoundException("There is no catagory for update with this id");

			_unitOfWork.gallaryCatagoryRepository.Delete(catagory);
			await _unitOfWork.SaveAsync();
		}
	}
}
