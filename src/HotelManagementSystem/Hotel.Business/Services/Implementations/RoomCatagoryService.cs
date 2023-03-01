namespace Hotel.Business.Services.Implementations
{
	public class RoomCatagoryService : IRoomCatagoryService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mappper;
		public RoomCatagoryService(IMapper mappper, IUnitOfWork unitOfWork)
		{
			_mappper = mappper;
			_unitOfWork = unitOfWork;
		}
		public async Task<List<RoomCatagoryDto>> GetAllAsync()
		{
			var list = await _unitOfWork.roomCatagoryRepository.GetAll().ToListAsync();
			var lists = _mappper.Map<List<RoomCatagoryDto>>(list);
			return lists;
		}

		public async Task<List<RoomCatagoryDto>> GetByCondition(Expression<Func<RoomCatagory, bool>> expression)
		{
			var list = await _unitOfWork.roomCatagoryRepository.GetAll().Where(expression).ToListAsync();
			var lists = _mappper.Map<List<RoomCatagoryDto>>(list);
			return lists;
		}

		public async Task<RoomCatagoryDto?> GetByIdAsync(int id)
		{
			var catagory = await _unitOfWork.roomCatagoryRepository.GetAll().Include(x=>x.Flats).FirstOrDefaultAsync(x=>x.Id==id);
			var catagoryDto = _mappper.Map<RoomCatagoryDto>(catagory);
			return catagoryDto;
		}
		public async Task Create(CreateRoomCatagoryDto entity)
		{
			RoomCatagory catagory = new();
			catagory.Name = entity.Name;
			var sameName = _unitOfWork.roomCatagoryRepository.GetByCondition(x => x.Name == entity.Name).ToList();
			if (sameName.Count >= 1) throw new RepeatedSameCatagoryNameException("Catagory Name exist");

			await _unitOfWork.roomCatagoryRepository.Create(catagory);
			await _unitOfWork.SaveAsync();
		}
		public async Task UpdateAsync(int id, UpdateRoomCatagoryDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id didnt match another");
			var catagory = await _unitOfWork.roomCatagoryRepository.GetByIdAsync(id);
			if (catagory is null) throw new NotFoundException("there is no catagory for update");
			catagory.Name = entity.Name;
			var sameNameList = _unitOfWork.roomCatagoryRepository.GetByCondition(x => x.Name == entity.Name).ToList();
			if (sameNameList.Count >= 1) throw new RepeatedSameCatagoryNameException("This catagory name exist");
			_unitOfWork.roomCatagoryRepository.Update(catagory);
			await _unitOfWork.SaveAsync();
		}

		public async Task Delete(int id)
		{
			var catagory = await _unitOfWork.roomCatagoryRepository.GetByIdAsync(id);
			if (catagory is null) throw new NotFoundException("there is no catagory for update");
			_unitOfWork.roomCatagoryRepository.Delete(catagory);
			await _unitOfWork.SaveAsync();
		}



	}

}
