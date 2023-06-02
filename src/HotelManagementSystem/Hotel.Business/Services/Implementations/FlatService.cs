namespace Hotel.Business.Services.Implementations
{
	public class FlatService : IFlatService
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public FlatService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
		public async Task<List<FlatDto>> GetAllAsync()
		{
			var list = await _unitOfWork.flatRepository.GetAll().ToListAsync();
			var lists = _mapper.Map<List<FlatDto>>(list);
			return lists;
		}

		public async Task<List<FlatDto>> GetByCondition(Expression<Func<Flat, bool>> expression)
		{

			var list = await _unitOfWork.flatRepository.GetAll().Where(expression).ToListAsync();
			var lists = _mapper.Map<List<FlatDto>>(list);
			return lists;
		}
		public async Task<FlatDto?> GetByIdAsync(int id)
		{
			var flat = await _unitOfWork.flatRepository.GetByIdAsync(id);
			if (flat is null) throw new NotFoundException("There is no flat for this id");
			var flatDto = _mapper.Map<FlatDto>(flat);
			return flatDto;
		}

		public async Task AddAmentityToFlat(int amentityId, int flatId)
		{
			var amentity = await _unitOfWork.amentityRepository.GetByIdAsync(amentityId);
			if (amentity is null) throw new NotFoundException("there is not amentity with this id");
			var flat = await _unitOfWork.flatRepository.GetAll().Include(x => x.Amentities).FirstOrDefaultAsync(x => x.Id == flatId);
			if (flat is null) throw new NotFoundException("there is not flat with this id");
			if (flat.Amentities != null)
			{
				foreach (var item in flat.Amentities)
				{
					if (item.AmentityId == amentity.Id) throw new RepeatedChoiceException("This option already exist");
				}
			}

			if (flat.Amentities != null && amentity.Flats != null)
			{
				flat.Amentities.Add(new FlatAmentity() { Amentity = amentity });
			}

			_unitOfWork.flatRepository.Update(flat);
			await _unitOfWork.SaveAsync();
		}

		public async Task DeleteAmentityFromFlat(int amentityId, int flatId)
		{
			var amentity = await _unitOfWork.amentityRepository.GetByIdAsync(amentityId);
			if (amentity is null) throw new NotFoundException("there is not amentity with this id");

			var flat = await _unitOfWork.flatRepository.GetAll().Include(x => x.Amentities).FirstOrDefaultAsync(x => x.Id == flatId);
			if (flat is null) throw new NotFoundException("there is not flat with this id");
			bool check = false;
			if (flat.Amentities is null) throw new NotFoundException("there is not any amentity for delete");
			foreach (var item in flat.Amentities)
			{
				if (item.AmentityId == amentity.Id)
				{
					check = true;
					flat.Amentities.Remove(item);
				}
			}
			if (check == false) throw new NotFoundException("there is not suitable amentity of flat");

			await _unitOfWork.SaveAsync();
		}
		public async Task UpdateAmentityOfFlat(int amentityId, int newAmentityId, int flatId)
		{
			var amentity = await _unitOfWork.amentityRepository.GetByIdAsync(amentityId);
			if (amentity is null) throw new NotFoundException("there is not amentity with this id");
			var NewAmentity = await _unitOfWork.amentityRepository.GetByIdAsync(newAmentityId);
			if (NewAmentity is null) throw new NotFoundException("there is not new amentity with this id");
			var flat = await _unitOfWork.flatRepository.GetAll().Include(x => x.Amentities).FirstOrDefaultAsync(x => x.Id == flatId);
			if (flat is null) throw new NotFoundException("there is not flat with this id");
			bool check = false;
			if (flat.Amentities is null) throw new NotFoundException("there is not any amentity for delete");
			foreach (var item in flat.Amentities)
			{
				if (item.AmentityId == amentity.Id)
				{
					check = true;
					flat.Amentities.Remove(item);
				}
			}
			if (check == false) throw new NotFoundException("there is not suitable amentity of this flat");

			if (flat.Amentities != null && amentity.Flats != null)
			{
				flat.Amentities.Add(new FlatAmentity() { Amentity = NewAmentity });
			}
			await _unitOfWork.SaveAsync();
		}
		public async Task Create(CreateFlatDto entity)
		{
			var room = _unitOfWork.roomCatagoryRepository.GetAll().Include(x => x.Flats).FirstOrDefault(c => c.Id == entity.RoomCatagoryId);
			if (room is null) throw new IncorrectIdException("there is no catagory with this id for to set");
			var flat = new Flat();
			flat.DiscountPercent = entity.DiscountPercent;
			flat.Price = entity.Price;
			flat.BedCount = entity.BedCount;
			flat.Description = entity.Description;
			flat.Name = entity.Name;
			flat.RoomCatagoryId = entity.RoomCatagoryId;
			flat.DiscountPrice = entity.Price * (100 - entity.DiscountPercent)/ 100;
			flat.RoomCatagory = room;
			flat.Rating = 5;
			if (room.Flats != null)
			{
				room.Flats.Add(flat);
			}
			await _unitOfWork.flatRepository.Create(flat);
			await _unitOfWork.SaveAsync();
		}
		public async Task UpdateAsync(int id, UpdateFlatDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("id didt match another");
			var flat =await _unitOfWork.flatRepository.GetByIdAsync(id);
			if (flat is null) throw new NotFoundException("there is no flat to update");
			var roomCatagory = _unitOfWork.roomCatagoryRepository.GetAll().FirstOrDefault(x => x.Id == entity.RoomCatagoryId);
			if (roomCatagory is null) throw new NotFoundException("there is no catagory with this id to update");
			
			flat.Size=entity.Size;
			flat.DiscountPercent= entity.DiscountPercent;
			flat.Price= entity.Price;
			flat.BedCount= entity.BedCount;
			flat.Description= entity.Description;
			flat.Name= entity.Name;
			flat.RoomCatagory = roomCatagory;
			flat.RoomCatagoryId= entity.RoomCatagoryId;
			flat.DiscountPrice = entity.Price*(100-entity.DiscountPercent) / 100;
			_unitOfWork.flatRepository.Update(flat);
			await _unitOfWork.SaveAsync();
		}

		public async Task Delete(int id)
		{
			var flat = _unitOfWork.flatRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (flat is null) throw new NotFoundException("there is no flat to delete");
			_unitOfWork.flatRepository.Delete(flat);
			await _unitOfWork.SaveAsync();
		}
	}
}
