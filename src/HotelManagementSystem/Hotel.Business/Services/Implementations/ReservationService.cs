namespace Hotel.Business.Services.Implementations
{
	public class ReservationService : IReservationService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;

		public ReservationService(IMapper mapper, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_userManager = userManager;
			_unitOfWork = unitOfWork;
		}
		public async Task<List<ReservationDto>> GetAllAsync()
		{
			var list = await _unitOfWork.reservationRepository.GetAll().Include(x => x.Flat).Include(y => y.AppUser).ToListAsync();
			List<ReservationDto> listDto = new();
			foreach (var item in list)
			{

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
						IsCanceled = item.IsCanceled,
						IsDeleted = item.IsDeleted
					});
				}
			}

			return listDto;
		}

		public async Task<List<ReservationDto>> GetByCondition(Expression<Func<Reservation, bool>> expression)
		{
			var list = await _unitOfWork.reservationRepository.GetAll().Include(x => x.Flat).Where(expression).ToListAsync();
			List<ReservationDto> listDto = new();
			foreach (var item in list)
			{
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
					});
				}
			}

			return listDto;
		}

		public async Task<ReservationDto?> GetByIdAsync(int reservId)
		{
			var list = await _unitOfWork.reservationRepository.GetAll().Include(x => x.Flat).Include(y => y.AppUser).FirstOrDefaultAsync(x => x.Id == reservId);
			ReservationDto reserv = null;
			if (list != null && list.Flat != null && list.AppUser != null)
			{
				reserv = new ReservationDto()
				{
					Id = list.Id,
					StartDate = list.StartDate,
					EndDate = list.EndDate,
					UserId = list.UserId,
					Price = list.Flat.Price,
					Adults = list.Adult,
					Children = list.Children,
					FlatId = list.FlatId,
				};
			};

			return reserv;
		}
		public async Task CreateRezerv(string UserId, DateTime CheckInDate, DateTime CheckOutDate, List<CreateReservationDto>? entities)
		{

			if (CheckInDate == CheckOutDate) throw new BadRequestException("check in date must different from check out");
			if (CheckInDate > CheckOutDate) throw new BadRequestException("check in date must before from check out date");
			if (CheckInDate < DateTime.Now || CheckOutDate < DateTime.Now) throw new BadRequestException("date must not chosen from past ");

			var user = await _userManager.FindByIdAsync(UserId);
			if (user is null) throw new NotFoundException("there is no user with this id");
			//var userInfo = await _UserRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id);
			//if (userInfo is null) throw new NotFoundException("reservation can not exist without user info");
			if (entities is null) throw new NotFoundException("reservation can not exist without flatId,adults,children elements ");
			foreach (var entity in entities)
			{
				var flat = await _unitOfWork.flatRepository.GetByIdAsync(entity.FlatId);
				if (flat is null) throw new NotFoundException("there is no flat with this id");
				float lastPrice = 0f;
				if (flat.DiscountPercent != 0)
				{
					lastPrice = flat.DiscountPrice;
				}
				else
				{
					lastPrice = flat.Price;
				}
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
					Price = timeDifferenceAsDays * lastPrice,
					IsCanceled = false,
					IsDeleted = false,
					IsFinished = false
				};
				await _unitOfWork.reservationRepository.Create(reservation);
			}
			await _unitOfWork.SaveAsync();
		}

		public async Task UpdateAsync(int id, UpdateReservationDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("id didnt overlap");
			var reserv = _unitOfWork.reservationRepository.GetAll().Include(r => r.AppUser).Include(r => r.Flat).FirstOrDefault(r => r.Id == id);
			if (reserv is null) throw new NotFoundException("there is no reservation with this id");
			var flat = await _unitOfWork.flatRepository.GetByIdAsync(entity.FlatId);
			if (flat is null) throw new NotFoundException("there is no flat with this id");
			var user = await _userManager.FindByIdAsync(entity.UserId);
			if (user is null) throw new NotFoundException("there is no user with this id");
			if (entity.Adults + entity.Children > flat.BedCount) throw new BadRequestException($" flat {flat.Id} may have maximum {flat.BedCount} guests");
			if (reserv.StartDate != entity.StartDate || reserv.EndDate != entity.EndDate || reserv.FlatId != entity.FlatId)
			{
				DateDto date = new()
				{
					CheckInDate = entity.StartDate,
					CheckOutDate = entity.EndDate,
				};
				var result = await IsReserve(entity.FlatId, date);
				if (result) throw new AlreadyExistException("this flat for these dates is not available");
			}

			reserv.Adult = entity.Adults;
			reserv.EndDate = entity.EndDate;
			reserv.StartDate = entity.StartDate;
			reserv.FlatId = entity.FlatId;
			reserv.UserId = entity.UserId;
			reserv.Children = entity.Children;
			reserv.Price = entity.Price;

			_unitOfWork.reservationRepository.Update(reserv);
			await _unitOfWork.SaveAsync();
		}

		public async Task Delete(int id)
		{
			var reserv = await _unitOfWork.reservationRepository.GetAll().FirstOrDefaultAsync(r => r.Id == id);
			if (reserv is null) throw new NotFoundException("there is no reservation with this id");
			reserv.IsDeleted = true;
			reserv.IsFinished = true;
			await _unitOfWork.SaveAsync();
		}

		public async Task CancelReservation(int reservId)
		{
			var reserv = await _unitOfWork.reservationRepository.GetAll().FirstOrDefaultAsync(r => r.Id == reservId);
			if (reserv is null) throw new NotFoundException("there is no reservation with this id");
			reserv.IsCanceled = true;
			reserv.IsFinished = true;
			reserv.IsDeleted = true;
			_unitOfWork.reservationRepository.Update(reserv);
			await _unitOfWork.SaveAsync();
		}

		public async Task<bool> IsReserve(int flatId, DateDto date)
		{
			bool available = true;
			var flat = await _unitOfWork.flatRepository.GetByIdAsync(flatId);
			if (flat is null) throw new NotFoundException("there is no flat with this id");
			var reservs = await _unitOfWork.reservationRepository.GetByCondition(r => r.FlatId == flatId).ToListAsync();
			if (reservs is null || reservs.Count == 0) { available = true; }
			else
			{
				foreach (var rezerv in reservs)
				{
					if ((rezerv.StartDate <= date.CheckOutDate && rezerv.EndDate >= date.CheckInDate) == true)
					{
						if (rezerv.IsFinished == true || rezerv.IsCanceled == true || rezerv.IsDeleted == true)
						{
							available = true;
						}
						else { available = false; }
					}
				}
			}
			return !available;
		}

		public async Task<List<AvailableFlatsDto>> AvailableFlatsForReserve(DateDto date)
		{
			bool isAvailable = false;
			List<AvailableFlatsDto> availableFlats = new();
			var flats = await _unitOfWork.flatRepository.GetAll().Include(f => f.RoomCatagory).ToListAsync();//5 
			foreach (var flat in flats)
			{

				var listReserv = await _unitOfWork.reservationRepository.GetAll().Where(x => x.FlatId == flat.Id).ToListAsync();
				if (listReserv.Count == 0) { availableFlats.Add(new() { CatagoryId = flat.RoomCatagoryId, FlatId = flat.Id }); }
				else
				{
					foreach (var reserv in listReserv)
					{

						if ((reserv.StartDate <= date.CheckOutDate && reserv.EndDate >= date.CheckInDate) == false)
						{
							isAvailable = true;
						}

						else
						{
							if (reserv.IsFinished == true || reserv.IsCanceled == true || reserv.IsDeleted == true)
							{
								isAvailable = true;
							}
							else { isAvailable = false; }
						}
						if (!isAvailable) break;

					}

					if (isAvailable)
					{
						availableFlats.Add(new() { CatagoryId = flat.RoomCatagoryId, FlatId = flat.Id });
					}


				}
			}
			return availableFlats;
		}
		public async Task FinishEndedReservations()
		{
			var reservs = await _unitOfWork.reservationRepository.GetAll().Where(e => e.EndDate < DateTime.Now).ToListAsync();
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
				var reserv = await _unitOfWork.reservationRepository.GetByIdAsync(id.ReservId);
				if (reserv is null) throw new NotFoundException("there is no reserv with this id");
				totalPrice += reserv.Price;
			}
			return totalPrice;
		}

		public async Task<List<RecomendedFlatDto>> RecomendedFlats(DateDto dateDto, int adults = 1, int children = 0)
		{

			bool isAvailable = false;
			bool isEqual = false;
			bool isLess = false;
			List<RecomendedFlatDto> recomendedFlats = new();
			List<RecomendedFlatDto> availableFlats = new();
			var total = adults + children;
			var flats = await _unitOfWork.flatRepository.GetAll().Include(f => f.RoomCatagory).ToListAsync();
			foreach (var flat in flats)
			{
				isEqual = (flat.BedCount == total);
				isAvailable = _unitOfWork.reservationRepository.GetAll().Where(x => x.FlatId == flat.Id).All(x => (x.IsCanceled == false && x.IsFinished == false && x.IsDeleted == false) && ((x.StartDate <= dateDto.CheckOutDate && x.EndDate >= dateDto.CheckInDate) == false));
				isLess = (flat.BedCount < total);
				if (isAvailable && isEqual)
				{
					recomendedFlats.Add(new() { CatagoryId = flat.RoomCatagoryId, FlatId = flat.Id, BedCount = flat.BedCount, Price = flat.Price });
					return recomendedFlats;
				}
				if (isAvailable)
				{
					availableFlats.Add(new() { CatagoryId = flat.RoomCatagoryId, FlatId = flat.Id, BedCount = flat.BedCount, Price = flat.Price });
				}
			}
			var gettedList = availableFlats.OrderByDescending(x => x.BedCount).ToList();
			int c = 0;
			recomendedFlats.Add(gettedList[c]);
			var neededCount = total - gettedList[c].BedCount;
			var getElement = gettedList.FirstOrDefault(x => x.BedCount <= gettedList[c].BedCount);
			if (getElement != null)
			{
				gettedList.Remove(getElement);
			}
			if (neededCount > 0)
			{
				while (c < gettedList.Count)
				{
					if (gettedList.Any(x => x.BedCount <= neededCount))
					{
						var recommend = gettedList.FirstOrDefault(x => x.BedCount <= neededCount);
						if (recommend != null)
						{
							recomendedFlats.Add(new() { CatagoryId = recommend.CatagoryId, FlatId = recommend.FlatId, BedCount = recommend.BedCount, Price = recommend.Price });
							gettedList.Remove(recommend);
							neededCount = neededCount - recommend.BedCount;
						}
						if (neededCount <= 0)
						{
							return recomendedFlats;
						}
					}
					else
					{
						neededCount = neededCount + 1;

					}
					c++;
				}
			}
			return recomendedFlats;
		}
	}
}
