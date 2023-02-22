using Hotel.Business.DTOs.ReservationDTOs;

namespace Hotel.Business.Services.Implementations
{
	public class ReservationService : IReservationService
	{
		private readonly IReservationRepository _repository;
		private readonly ISelectedListRepository _ListRepository;
		private readonly IMapper _mapper;

		public ReservationService(IMapper mapper, IReservationRepository repository, ISelectedListRepository listRepository)
		{
			_mapper = mapper;
			_repository = repository;
			_ListRepository = listRepository;
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
						GuestName = item.AppUser.UserInfo.FirstName + item.AppUser.UserInfo.LastName
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
		public async Task CreateRezerv(CreateReservationDto entity)
		{
			
		}

		public /*async*/ Task UpdateAsync(int id, UpdateCommentDto entity)
		{
			throw new NotImplementedException();
		}
		public /*async*/ Task Delete(int id)
		{
			throw new NotImplementedException();
		}

	}
}
