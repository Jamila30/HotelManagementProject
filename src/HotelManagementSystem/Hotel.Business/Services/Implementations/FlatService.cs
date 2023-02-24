﻿using Hotel.DataAccess.Contexts;

namespace Hotel.Business.Services.Implementations
{
	public class FlatService : IFlatService
	{

		private readonly IFlatRepository _repository;
		private readonly IRoomCatagoryRepository _roomCatagoryRepo;
		private readonly IAmentityRepository _amentRepo;
		private readonly IMapper _mapper;
		public FlatService(IFlatRepository repository, IMapper mapper, IRoomCatagoryRepository roomCatagoryRepo, IAmentityRepository amentRepo)
		{
			_repository = repository;
			_mapper = mapper;
			_roomCatagoryRepo = roomCatagoryRepo;
			_amentRepo = amentRepo;
		}
		public async Task<List<FlatDto>> GetAllAsync()
		{
			var list = await _repository.GetAll().ToListAsync();
			var lists = _mapper.Map<List<FlatDto>>(list);
			return lists;
		}

		public async Task<List<FlatDto>> GetByCondition(Expression<Func<Flat, bool>> expression)
		{

			var list = await _repository.GetAll().Where(expression).ToListAsync();
			var lists = _mapper.Map<List<FlatDto>>(list);
			return lists;
		}
		public async Task<FlatDto?> GetByIdAsync(int id)
		{
			var flat = await _repository.GetByIdAsync(id);
			if (flat is null) throw new NotFoundException("There is no flat for this id");
			var flatDto = _mapper.Map<FlatDto>(flat);
			return flatDto;
		}
		
		public async Task AddAmentityToFlat(int amentityId, int flatId)
		{
			var amentity = await _amentRepo.GetByIdAsync(amentityId);
			if (amentity is null) throw new NotFoundException("there is not amentity with this id");
			var flat = await _repository.GetAll().Include(x => x.Amentities).FirstOrDefaultAsync(x => x.Id == flatId);
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

			_repository.Update(flat);
			await _repository.SaveChanges();
		}

		public async Task DeleteAmentityFromFlat(int amentityId, int flatId)
		{
			var amentity = await _amentRepo.GetByIdAsync(amentityId);
			if (amentity is null) throw new NotFoundException("there is not amentity with this id");

			var flat = await _repository.GetAll().Include(x => x.Amentities).FirstOrDefaultAsync(x => x.Id == flatId);
			if (flat is null) throw new NotFoundException("there is not flat with this id");
			bool check=false;
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

			await _repository.SaveChanges();
		}
		public async Task UpdateAmentityOfFlat(int amentityId, int newAmentityId, int flatId)
		{
			var amentity = await _amentRepo.GetByIdAsync(amentityId);
			if (amentity is null) throw new NotFoundException("there is not amentity with this id");
			var NewAmentity = await _amentRepo.GetByIdAsync(newAmentityId);
			if (NewAmentity is null) throw new NotFoundException("there is not new amentity with this id");
			var flat = await _repository.GetAll().Include(x => x.Amentities).FirstOrDefaultAsync(x => x.Id == flatId);
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
			await _repository.SaveChanges();
		}
		public async Task Create(CreateFlatDto entity)
		{
			var room = _roomCatagoryRepo.GetAll().Include(x => x.Flats).FirstOrDefault(c => c.Id == entity.RoomCatagoryId);
			if (room is null) throw new IncorrectIdException("there is no catagory with this id for to set");
			var flat = _mapper.Map<Flat>(entity);
			flat.RoomCatagory = room;
			if (room.Flats != null)
			{
				room.Flats.Add(flat);
			}
			await _repository.Create(flat);
			await _repository.SaveChanges();

		}
		public async Task UpdateAsync(int id, UpdateFlatDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("id didt match another");
			var flat = _repository.GetByCondition(x => x.Id == id);
			if (flat is null) throw new NotFoundException("there is no flat to update");
			var roomCatagory = _repository.GetAll().FirstOrDefault(x => x.RoomCatagoryId == entity.RoomCatagoryId);
			if (roomCatagory is null) throw new NotFoundException("there is no catagory with this id to update");
			var newFlat = _mapper.Map<Flat>(entity);
			_repository.Update(newFlat);
			await _repository.SaveChanges();

		}

		public async Task Delete(int id)
		{
			var flat = _repository.GetAll().FirstOrDefault(x => x.Id == id);
			if (flat is null) throw new NotFoundException("there is no flat to delete");
			_repository.Delete(flat);
			await _repository.SaveChanges();
		}


	}
}