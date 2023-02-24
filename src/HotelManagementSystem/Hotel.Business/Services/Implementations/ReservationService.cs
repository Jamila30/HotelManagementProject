﻿using Hotel.Business.DTOs.ReservationDTOs;

namespace Hotel.Business.Services.Implementations
{
	public class ReservationService : IReservationService
	{
		private readonly IReservationRepository _repository;
		private readonly ISelectedListRepository _ListRepository;
		private readonly IFlatRepository _flatRepository;
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;

		public ReservationService(IMapper mapper, IReservationRepository repository, ISelectedListRepository listRepository, IFlatRepository flatRepository, UserManager<AppUser> userManager)
		{
			_mapper = mapper;
			_repository = repository;
			_ListRepository = listRepository;
			_flatRepository = flatRepository;
			_userManager = userManager;
		}
		public async Task<List<ReservationDto>> GetAllAsync()
		{
			var list = await _repository.GetAll().Include(x => x.Flat).Include(y => y.AppUser).ThenInclude(x => x.UserInfo).ToListAsync();
			List<ReservationDto> listDto = new();
			foreach (var item in list)
			{
				if (item.Flat != null && item.AppUser != null && item.AppUser.UserInfo != null)
				{
					listDto.Add(new ReservationDto()
					{
						Id = item.Id,
						StartDate = item.StartDate,
						EndDate = item.EndDate,
						UserId = item.UserId,
						Price = item.Flat.Price,
						Adults = item.Adult,
						Children = item.Children,
						FlatId = item.FlatId,
						GuestName = item.AppUser.UserInfo.FirstName + item.AppUser.UserInfo.LastName,
						IsCanceled= item.IsCanceled,
						IsDeleted= item.IsDeleted
					});
				}
			}

			return listDto;
		}

		public async Task<List<ReservationDto>> GetByCondition(Expression<Func<Reservation, bool>> expression)
		{
			var list = await _repository.GetAll().Where(expression).Include(x => x.Flat).Include(y => y.AppUser).ThenInclude(x => x.UserInfo).ToListAsync();
			List<ReservationDto> listDto = new();
			foreach (var item in list)
			{
				if (item.Flat != null && item.AppUser != null && item.AppUser.UserInfo != null)
				{
					listDto.Add(new ReservationDto()
					{
						Id = item.Id,
						StartDate = item.StartDate,
						EndDate = item.EndDate,
						UserId = item.UserId,
						Price = item.Flat.Price,
						Adults = item.Adult,
						Children = item.Children,
						FlatId = item.FlatId,
						GuestName = item.AppUser.UserInfo.FirstName + item.AppUser.UserInfo.LastName
					});
				}
			}

			return listDto;
		}

		public async Task<ReservationDto?> GetByIdAsync(int reservId)
		{
			var list = await _repository.GetAll().Include(x => x.Flat).Include(y => y.AppUser).ThenInclude(x => x.UserInfo).FirstOrDefaultAsync(x => x.Id == reservId);
			var reserv = new ReservationDto();
			if (list != null && list.Flat != null && list.AppUser != null && list.AppUser.UserInfo != null)
			{
				reserv.Id = list.Id;
				reserv.StartDate = list.StartDate;
				reserv.EndDate = list.EndDate;
				reserv.UserId = list.UserId;
				reserv.Price = list.Flat.Price;
				reserv.Adults = list.Adult;
				reserv.Children = list.Children;
				reserv.FlatId = list.FlatId;
				reserv.GuestName = list.AppUser.UserInfo.FirstName + list.AppUser.UserInfo.LastName;
			};

			return reserv;
		}
		public async Task CreateRezerv(StabilPropertirsDto stabil, List<CreateReservationDto> entities)
		{
			var user = await _userManager.FindByIdAsync(stabil.UserId);
			if (user is null) throw new NotFoundException("there is no user with this id");
			foreach (var entity in entities)
			{
				var flat = await _flatRepository.GetByIdAsync(entity.FlatId);
				if (flat is null) throw new NotFoundException("there is no flat with this id");
				if (entity.Adults > flat.Adults) throw new BadRequestException($"this flat may have maximum {flat.Adults} adults");
				if (entity.Children > flat.Children) throw new BadRequestException($"this flat may have maximum {flat.Adults} children");
				var timeDifferenceAsDays = (stabil.CheckOutDate - stabil.CheckInDate).Days;
				Reservation reservation = new()
				{
					Adult = entity.Adults,
					Children = entity.Children,
					FlatId = entity.FlatId,
					Flat = flat,
					AppUser = user,
					UserId = stabil.UserId,
					StartDate = stabil.CheckInDate,
					EndDate = stabil.CheckOutDate,
					Price = timeDifferenceAsDays * flat.Price,
					IsCanceled = false,
					IsDeleted = false,
					IsFinished = false
				};
				await _repository.Create(reservation);
			}
			await _repository.SaveChanges();
		}

		public async Task UpdateAsync(int id, UpdateReservationDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("id didnt overlap");
			var reserv = _repository.GetAll().Include(r => r.AppUser).Include(r => r.Flat).Single(r => r.Id == id);
			if (reserv is null) throw new NotFoundException("there is no reservation with this id");
			var flat = await _flatRepository.GetByIdAsync(entity.FlatId);
			if (flat is null) throw new NotFoundException("there is no flat with this id");
			var user = await _userManager.FindByIdAsync(entity.UserId);
			if (user is null) throw new NotFoundException("there is no user with this id");
			if (entity.Adults > flat.Adults) throw new BadRequestException($"this flat may have maximum {flat.Adults} adults");
			if (entity.Children > flat.Children) throw new BadRequestException($"this flat may have maximum {flat.Adults} children");
			if (reserv.StartDate != entity.StartDate || reserv.EndDate != entity.EndDate || reserv.FlatId != entity.FlatId)
			{
				DateDto date = new DateDto();
				date.CheckInDate=entity.StartDate;
				date.CheckOutDate = entity.EndDate;
				var result = await IsReserved(entity.FlatId, date);
				if (!result) throw new AlreadyExistException("this flat for these dates is not available");
			}
			var newReservation = _mapper.Map<Reservation>(entity);
			_repository.Update(newReservation);
			await _repository.SaveChanges();
		}
		public async Task Delete(int id)
		{
			var reserv = await _repository.GetAll().Include(r => r.AppUser).Include(r => r.Flat).SingleAsync(r => r.Id == id);
			if (reserv is null) throw new NotFoundException("there is no reservation with this id");
			reserv.IsDeleted = true;
			reserv.IsFinished= true;
		}

		public async Task CancelReservation(int reservId)
		{
			var reserv = await _repository.GetAll().Include(r => r.AppUser).Include(r => r.Flat).SingleAsync(r => r.Id == reservId);
			if (reserv is null) throw new NotFoundException("there is no reservation with this id");
			reserv.IsCanceled = true;
			reserv.IsFinished = true;
			
		}

		public async Task<bool> IsReserved(int flatId, DateDto date)
		{
			bool available = true;
			var reservs = await _repository.GetByCondition(r => r.FlatId == flatId).ToListAsync();
			if (reservs != null && reservs.Count()>0)
			{
				foreach (var rezerv in reservs)
				{
					if (!((date.CheckOutDate > rezerv.StartDate) && (date.CheckInDate > rezerv.EndDate) && rezerv.IsFinished == false)) { available = false; };
				}
			}
			return available;
		}

		public async Task<List<AvailableFlatsDto>> AvailableFlatsForReserve(DateDto date)
		{
			bool isAvailable = false;
			List<AvailableFlatsDto> availableFlats = new List<AvailableFlatsDto>();
			var flats = await _flatRepository.GetAll().Include(f => f.RoomCatagory).ToListAsync();
			foreach (var flat in flats)
			{
				isAvailable = _repository.GetAll().Where(x => x.FlatId == flat.Id).All(x => x.StartDate < date.CheckOutDate && x.EndDate < date.CheckInDate && x.IsFinished==true);
				if (isAvailable)
				{
					availableFlats.Add(new() { CatagoryId = flat.RoomCatagoryId, FlatId = flat.Id });
				}
			}

			return availableFlats;
		}
		public async Task FinishEndedReservations()
		{
			var reservs=await _repository.GetAll().Where(e => e.EndDate<DateTime.Now).ToListAsync();
			foreach (var item in reservs)
			{
				item.IsFinished = true;
			}
		}
		public async Task<float> GetTotalPrice(List<int> reservIds)
		{
			var totalPrice = 0f;	
			foreach (var id in reservIds)
			{
				var reserv=await _repository.GetByIdAsync(id);
				if (reserv is null) throw new NotFoundException("there is no reserv with this id");
				totalPrice += reserv.Price;
			}
			return totalPrice;
		}

		//public async Task<List<RecomendedFlatDto>> RecomendedFlats(DateDto dateDto, int adults = 1, int children = 0)
		//{

		//	bool isAvailable = false;
		//	bool isEqual = false;
		//	bool isLess = false;
		//	List<RecomendedFlatDto> recomendedFlats = new List<RecomendedFlatDto>();
		//	var total = adults + children;
		//	var flats = await _flatRepository.GetAll().Include(f => f.RoomCatagory).ToListAsync();
		//	foreach (var flat in flats)
		//	{
		//		isEqual = (flat.BedCount == total);
		//		isAvailable = _repository.GetAll().Where(x => x.FlatId == flat.Id).All(x => x.StartDate < dateDto.CheckOutDate && x.EndDate < dateDto.CheckInDate);
		//		isLess = (flat.BedCount < total);
		//		if (isAvailable && isEqual)
		//		{
		//			recomendedFlats.Add(new() { CatagoryId = flat.RoomCatagoryId, FlatId = flat.Id, BedCount = flat.BedCount, Price = flat.Price });
		//			return recomendedFlats;
		//		}
		//		else if (isAvailable && isLess)
		//		{
		//			List<int> listId = new List<int>();
		//			var need = total - flat.BedCount;
		//			recomendedFlats.Add(new() { CatagoryId = flat.RoomCatagoryId, FlatId = flat.Id, BedCount = flat.BedCount, Price = flat.Price });
		//			listId.Add(flat.BedCount);
		//			return recomendedFlats.OrderByDescending(f => f.BedCount).ToList();
		//		}
		//		return recomendedFlats.OrderByDescending(f => f.BedCount).ToList();
		//	}
		//}
	}
}