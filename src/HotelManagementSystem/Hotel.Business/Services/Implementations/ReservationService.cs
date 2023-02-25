using Hotel.Business.DTOs.ReservationDTOs;

namespace Hotel.Business.Services.Implementations
{
	public class ReservationService : IReservationService
	{
		private readonly IReservationRepository _repository;
		private readonly IUserInfoRepository _UserRepository;
		private readonly IFlatRepository _flatRepository;
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;

		public ReservationService(IMapper mapper, IReservationRepository repository, IUserInfoRepository userRepo, IFlatRepository flatRepository, UserManager<AppUser> userManager)
		{
			_mapper = mapper;
			_repository = repository;
			_UserRepository = userRepo;
			_flatRepository = flatRepository;
			_userManager = userManager;
		}
		public async Task<List<ReservationDto>> GetAllAsync()
		{
			var list = await _repository.GetAll().Include(x => x.Flat).Include(y => y.AppUser).ThenInclude(x => x.UserInfo).ToListAsync();
			List<ReservationDto> listDto = new List<ReservationDto>();
			foreach (var item in list)
			{
				var userInfo = await _UserRepository.GetAll().SingleAsync(x => x.UserId == item.UserId);
				if (userInfo is null) throw new NotFoundException("reservation can not exist without user info");
				if (item.Flat != null)
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
						GuestName = userInfo.FirstName + userInfo.LastName,
						IsCanceled = item.IsCanceled,
						IsDeleted = item.IsDeleted
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
		public async Task CreateRezerv(DateTime CheckInDate, DateTime CheckOutDate, string UserId, List<CreateReservationDto> entities)
		{
			if (CheckInDate == CheckOutDate) throw new BadRequestException("check in date must different from check out");
			if (CheckInDate > CheckOutDate) throw new BadRequestException("check in date must before from check out date");
			if (CheckInDate < DateTime.Now || CheckOutDate < DateTime.Now) throw new BadRequestException("date must not chosen from past ");
			var user = await _userManager.FindByIdAsync(UserId);
			if (user is null) throw new NotFoundException("there is no user with this id");
			foreach (var entity in entities)
			{
				var flat = await _flatRepository.GetByIdAsync(entity.FlatId);
				if (flat is null) throw new NotFoundException("there is no flat with this id");
				if ((entity.Adults + entity.Children) > flat.BedCount) throw new BadRequestException($"this flat may have maximum {flat.BedCount} guest");
				var timeDifferenceAsDays = (CheckOutDate - CheckInDate).Days;
				Reservation reservation = new()
				{
					Adult = entity.Adults,
					Children = entity.Children,
					FlatId = entity.FlatId,
					Flat = flat,
					AppUser = user,
					UserId = UserId,
					StartDate = CheckInDate,
					EndDate = CheckOutDate,
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
			if (entity.Adults + entity.Children > flat.BedCount) throw new BadRequestException($" flat {flat.Id} may have maximum {flat.BedCount} guests");
			if (reserv.StartDate != entity.StartDate || reserv.EndDate != entity.EndDate || reserv.FlatId != entity.FlatId)
			{
				DateDto date = new DateDto();
				date.CheckInDate = entity.StartDate;
				date.CheckOutDate = entity.EndDate;
				var result = await CanReserve(entity.FlatId, date);
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
			reserv.IsFinished = true;
			await _repository.SaveChanges();
		}

		public async Task CancelReservation(int reservId)
		{
			var reserv = await _repository.GetAll().FirstOrDefaultAsync(r => r.Id == reservId);
			if (reserv is null) throw new NotFoundException("there is no reservation with this id");
			reserv.IsCanceled = true;
			reserv.IsFinished = true;
			_repository.Update(reserv);
			await _repository.SaveChanges();
		}

		public async Task<bool> CanReserve(int flatId, DateDto date)
		{
			bool available = true;
			var flat = await _flatRepository.GetByIdAsync(flatId);
			if (flat is null) throw new NotFoundException("there is no flat with this id");
			var reservs = await _repository.GetByCondition(r => r.FlatId == flatId).ToListAsync();
			if (reservs is null || reservs.Count() == 0) { available = false; }
			else
			{
				foreach (var rezerv in reservs)
				{
					if (((rezerv.StartDate <= date.CheckOutDate && rezerv.EndDate >= date.CheckInDate) == true))
					{
						if (rezerv.IsFinished == true || rezerv.IsCanceled == true || rezerv.IsDeleted != true)
						{
							available = true;
						}
						else { available = false; }
					}
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

				var listReserv = await _repository.GetAll().Where(x => x.FlatId == flat.Id).ToListAsync();
				if (listReserv.Count() == 0) { availableFlats.Add(new() { CatagoryId = flat.RoomCatagoryId, FlatId = flat.Id }); }
				foreach (var reserv in listReserv)
				{

					if ((reserv.StartDate <= date.CheckOutDate && reserv.EndDate >= date.CheckInDate) == false)
					{
						isAvailable = true;
					}

					else
					{
						if (reserv.IsFinished == true || reserv.IsCanceled == true || reserv.IsDeleted != true)
						{
							isAvailable = true;
						}
						else { isAvailable = false; }
					}
				}
				if (isAvailable)
				{
					availableFlats.Add(new() { CatagoryId = flat.RoomCatagoryId, FlatId = flat.Id });
				}
			}

			return availableFlats;
		}
		public async Task FinishEndedReservations()
		{
			var reservs = await _repository.GetAll().Where(e => e.EndDate < DateTime.Now).ToListAsync();
			foreach (var item in reservs)
			{
				item.IsFinished = true;
			}
		}
		public async Task<float> GetTotalPrice(List<StabilPropertirsDto> reservIds)
		{
			var totalPrice = 0f;
			foreach (var id in reservIds)
			{
				var reserv = await _repository.GetByIdAsync(id.ReservId);
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
