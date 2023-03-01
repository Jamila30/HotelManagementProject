namespace Hotel.Business.Services.Implementations
{
	public class SelectedListService : ISelectedListService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public SelectedListService(ISelectedListRepository repository, IMapper mapper,IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<List<SelectedListDto>> GetAllAsync()
		{
			var list = await _unitOfWork.selectedListRepository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<SelectedListDto>>(list);
			return listDto;
		}

		public async Task<List<SelectedListDto>> GetByCondition(Expression<Func<SelectedList, bool>> expression)
		{
			var list = await _unitOfWork.selectedListRepository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<SelectedListDto>>(list);
			return listDto;
		}

		public async Task<SelectedListDto?> GetByIdAsync(int id)
		{
			var list = await _unitOfWork.selectedListRepository.GetAll().SingleOrDefaultAsync(l => l.Id == id);
			var listDto = _mapper.Map<SelectedListDto>(list);
			return listDto;
		}
		public async Task AddToList(List<int> flatIds)
		{
			foreach (var id in flatIds)
			{
				var flat = await _unitOfWork.flatRepository.GetAll().Include(f => f.RoomCatagory).Include(f => f.Images).SingleOrDefaultAsync(x => x.Id == id);
				if (flat is null) throw new NotFoundException("There is no flat with this id");
				var existFlats = _unitOfWork.selectedListRepository.GetByCondition(l => l.FlatId == id);
				var existId = id;
				if (existFlats.Count() > 0) throw new AlreadyExistException($" flat with {existId} already exists in selected list");
				SelectedList selectedList = new();
				if (flat.RoomCatagory != null && flat.Images != null)
				{
					selectedList.CatagoryName = flat.RoomCatagory.Name;
					selectedList.FlatId = flat.Id;
					if(flat.DiscountPercent==0)
					{
						selectedList.Price = flat.Price;
					}
					else
					{
						selectedList.Price = flat.DiscountPrice;
					}
					selectedList.Flat = flat;
					selectedList.CatagoryId = flat.RoomCatagoryId;
				};
				await _unitOfWork.selectedListRepository.Create(selectedList);
			}
			await _unitOfWork.SaveAsync();
		}

		public async Task UpdateAsync(int catagoryId, UpdateSelectedListDto updateList)
		{
			List<int> nextFlats = new ();
			if (catagoryId != updateList.CatagoryId) throw new IncorrectIdException("id didn't overlap");
			var list = await _unitOfWork.selectedListRepository.GetAll().Where(l => l.Flat != null ? l.Flat.RoomCatagoryId == catagoryId : false).ToListAsync();
			if (list.Count() == 0) throw new NotFoundException("There is no selected element for this catagory");
			var listCount = list.Count();
			if (updateList.FlatIds is null) throw new BadRequestException("updated list must contains at least 1 element");
			int flatIdCount = updateList.FlatIds.Count();
			
			if (flatIdCount > listCount)
			{
				for (int i = listCount; i < flatIdCount; i++)
				{
					nextFlats.Add(updateList.FlatIds[i]);
					await AddToList(nextFlats);
				}
			}
			else
			{
				for (int i = flatIdCount; i < listCount; i++)
				{
					_unitOfWork.selectedListRepository.Delete(list[i]);
				}
			}
			await _unitOfWork.SaveAsync();
		}
		public async Task DeleteOfOneCatagory(int catagoryId)
		{
			var listAll = await _unitOfWork.selectedListRepository.GetAll().Include(l => l.Flat).ThenInclude(l => l.RoomCatagory).Where(x => x.Flat != null ? x.Flat.RoomCatagoryId == catagoryId : false).ToListAsync();
			if (listAll.Count == 0) throw new BadRequestException("there is no element for delete");
			foreach (var list in listAll)
			{
				_unitOfWork.selectedListRepository.Delete(list);
			}
			await _unitOfWork.SaveAsync();
		}

		public async Task<float> GetTotalPrice()
		{
			float totalPrice = 0f;
			await _unitOfWork.selectedListRepository.GetAll().Include(x=>x.Flat).ForEachAsync(x => {
				if (x.Flat.DiscountPercent == 0)
				{
				totalPrice += x.Price; 
				}
				else
				{
					totalPrice += x.Flat.DiscountPrice;
				}
			});
			return totalPrice;
		}
		public async Task DeleteAllListItems()
		{
			var list = await _unitOfWork.selectedListRepository.GetAll().ToListAsync();
			foreach (var item in list)
			{
				_unitOfWork.selectedListRepository.Delete(item);
			}
			await _unitOfWork.SaveAsync();
		}
	}
}
