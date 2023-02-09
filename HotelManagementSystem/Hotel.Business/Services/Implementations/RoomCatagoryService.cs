using Hotel.Business.DTOs.RoomCatagoryDTOs;

namespace Hotel.Business.Services.Implementations
{
	public class RoomCatagoryService : IRoomCatagoryService
	{
		private readonly IRoomCatagoryRepository _repository;
		private readonly IMapper _mappper;
		public RoomCatagoryService(IRoomCatagoryRepository repository, IMapper mappper)
		{
			_repository = repository;
			_mappper = mappper;
		}
		public async Task<List<RoomCatagoryDto>> GetAllAsync()
		{
			var list = _repository.GetAll().ToList();
			var lists = _mappper.Map<List<RoomCatagoryDto>>(list);
			return lists;
		}

		public async Task<List<RoomCatagoryDto>> GetByCondition(Expression<Func<RoomCatagory, bool>> expression)
		{
			var list = _repository.GetAll().Where(expression).ToList();
			var lists = _mappper.Map<List<RoomCatagoryDto>>(list);
			return lists;
		}

		public async Task<RoomCatagoryDto?> GetByIdAsync(int id)
		{
			var catagory = await _repository.GetByIdAsync(id);
			var catagoryDto = _mappper.Map<RoomCatagoryDto>(catagory);
			return catagoryDto;
		}
		public async Task Create(CreateRoomCatagoryDto entity)
		{
			RoomCatagory catagory = new();
			catagory.Name = entity.Name;
			var sameName = _repository.GetByCondition(x => x.Name == entity.Name).ToList();
			if (sameName.Count >= 1) throw new RepeatedSameCatagoryNameException("Catagory Name exist");

			await _repository.Create(catagory);
			await _repository.SaveChanges();
		}
		public async Task UpdateAsync(int id, UpdateRoomCatagoryDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id didnt match another");
			var catagory = await _repository.GetByIdAsync(id);
			if (catagory is null) throw new NotFoundException("there is no catagory for update");
			catagory.Name = entity.Name;
			var sameNameList = _repository.GetByCondition(x => x.Name == entity.Name).ToList();
			if (sameNameList.Count >= 1) throw new RepeatedSameCatagoryNameException("This catagory name exist");
			_repository.Update(catagory);
			await _repository.SaveChanges();
		}

		public async Task Delete(int id)
		{
			var catagory = await _repository.GetByIdAsync(id);
			if (catagory is null) throw new NotFoundException("there is no catagory for update");
			_repository.Delete(catagory);
			await _repository.SaveChanges();
		}



	}

}
