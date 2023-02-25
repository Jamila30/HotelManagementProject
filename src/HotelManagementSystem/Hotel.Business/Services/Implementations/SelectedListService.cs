using Hotel.Business.DTOs.SelectedListDTOs;

namespace Hotel.Business.Services.Implementations
{
	public class SelectedListService : ISelectedListService
	{
		private readonly ISelectedListRepository _repository;
		private readonly IFlatRepository _flatRepository;
		private readonly IMapper _mapper;
		public SelectedListService(ISelectedListRepository repository, IMapper mapper, IFlatRepository flatRepository)
		{
			_repository = repository;
			_mapper = mapper;
			_flatRepository = flatRepository;
		}

		public async Task<List<SelectedListDto>> GetAllAsync()
		{
			var list = await _repository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<SelectedListDto>>(list);
			return listDto;
		}

		public async Task<List<SelectedListDto>> GetByCondition(Expression<Func<SelectedList, bool>> expression)
		{
			var list = await _repository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<SelectedListDto>>(list);
			return listDto;
		}

		public async Task<SelectedListDto?> GetByIdAsync(int id)
		{
			var list = await _repository.GetAll().SingleOrDefaultAsync(l => l.Id == id);
			var listDto = _mapper.Map<SelectedListDto>(list);
			return listDto;
		}
		public async Task AddToList(List<int> flatIds)
		{
			foreach (var id in flatIds)
			{
				var flat = await _flatRepository.GetAll().Include(f => f.RoomCatagory).Include(f => f.Images).SingleOrDefaultAsync(x => x.Id == id);
				if (flat is null) throw new NotFoundException("There is no flat with this id");
				var existFlats = _repository.GetByCondition(l => l.FlatId == id);
				var existId = id;
				if (existFlats.Count() > 0) throw new AlreadyExistException($" flat with {existId} already exists in selected list");
				SelectedList selectedList = new();
				if (flat.RoomCatagory != null && flat.Images != null)
				{
					selectedList.CatagoryName = flat.RoomCatagory.Name;
					selectedList.FlatId = flat.Id;
					selectedList.Price = flat.Price;
					selectedList.Flat = flat;
					selectedList.CatagoryId = flat.RoomCatagoryId;
				};
				await _repository.Create(selectedList);
			}
			await _repository.SaveChanges();
		}

		public async Task UpdateAsync(int catagoryId, UpdateSelectedListDto updateList)
		{
			List<int> nextFlats = new List<int>();
			if (catagoryId != updateList.CatagoryId) throw new IncorrectIdException("id didn't overlap");
			var list = await _repository.GetAll().Where(l => l.Flat != null ? l.Flat.RoomCatagoryId == catagoryId : false).ToListAsync();
			if (list.Count() == 0) throw new NotFoundException("There is no selected element for this catagory");
			var listCount = list.Count();
			var flatIdCount = updateList.FlatIds.Count();
			if (flatIdCount > listCount)
			{
				for (int i = listCount ; i < flatIdCount; i++)
				{
					nextFlats.Add(updateList.FlatIds[i]);
					await AddToList(nextFlats);
				}
			}
			else
			{
				for (int i = flatIdCount; i < listCount; i++)
				{
					_repository.Delete(list[i]);
				}
			}
			await _repository.SaveChanges();
		}
		public async Task DeleteOfOneCatagory(int catagoryId)
		{
			var listAll = await _repository.GetAll().Include(l => l.Flat).ThenInclude(l => l.RoomCatagory).Where(x => x.Flat != null ? x.Flat.RoomCatagoryId == catagoryId : false).ToListAsync();
			if (listAll.Count < 0) throw new BadRequestException("there is no element for delete");
			foreach (var list in listAll)
			{
				_repository.Delete(list);
			}
			await _repository.SaveChanges();
		}

		public async Task<float> GetTotalPrice()
		{
			float totalPrice = 0f;
			await _repository.GetAll().ForEachAsync(x => { totalPrice += x.Price; });
			return totalPrice;
		}
		public async Task DeleteAllListItems()
		{
			var list = await _repository.GetAll().ToListAsync();
			foreach (var item in list)
			{
				_repository.Delete(item);
			}
			await _repository.SaveChanges();
		}
	}
}
