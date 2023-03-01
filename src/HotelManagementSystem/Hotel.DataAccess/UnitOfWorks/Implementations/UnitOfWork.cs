namespace Hotel.DataAccess.UnitOfWorks.Implementations
{
	public class UnitOfWork:IUnitOfWork
	{
		private readonly AppDbContext _context;
		private readonly AccountRepository _accountRepository =null!;
		private readonly AmentityRepository _amentityRepository = null!;
		private readonly CommentRepository _commentRepository = null!;
		private readonly FAQRepository _fAQRepository = null!;
		private readonly FlatRepository _flatRepository = null!;
		private readonly GallaryCatagoryRepository _gallaryCatagoryRepository = null!;
		private readonly GallaryImageRepository _gallaryImageRepository = null!;
		private	readonly NearPlaceRepository _nearPlaceRepository = null!;
		private readonly ReservationRepository _reservationRepository = null!;
		private readonly RoomCatagoryRepository _roomCatagoryRepository = null!;
		private readonly RoomImageRepository _roomImageRepository = null! ;
		private readonly SelectedListRepository _selectedListRepository = null! ;
		private	readonly SentQuestionRepository _sentQuestionRepository = null! ;
		private readonly ServiceImageRepository _serviceImageRepository = null! ;
		private readonly ServiceOfferRepository _serviceOfferRepository = null! ;
		private readonly SettingsTableRepository _settingTableRepository = null! ;
		private readonly SliderHomeRepository _sliderHomeRepository = null!;
		private readonly TeamMemberRepository _teamMemberRepository = null!;
		private readonly TeamMemberInfoRepository _teamMemberInfoRepository = null!;
		private readonly UserInfoRepository _userInfoRepository = null!;
		private readonly WhyUsRepository _whyUsRepository = null!;
		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		public IAccountRepository accountRepository => _accountRepository ?? new AccountRepository(_context);
		public IAmentityRepository amentityRepository => _amentityRepository ?? new AmentityRepository(_context);
		public ICommentRepository commentRepository => _commentRepository ?? new CommentRepository(_context);
		public IFAQRepository fAQRepository =>_fAQRepository?? new FAQRepository(_context);
		public IFlatRepository flatRepository => _flatRepository ?? new FlatRepository(_context);
		public IGallaryCatagoryRepository gallaryCatagoryRepository =>_gallaryCatagoryRepository?? new GallaryCatagoryRepository(_context);
		public IGallaryImageRepository gallaryImageRepository => _gallaryImageRepository ?? new GallaryImageRepository(_context);
		public INearPlaceRepository nearPlaceRepository =>_nearPlaceRepository?? new NearPlaceRepository(_context);
		public IReservationRepository reservationRepository =>_reservationRepository?? new ReservationRepository(_context);
		public IRoomCatagoryRepository roomCatagoryRepository => _roomCatagoryRepository ?? new RoomCatagoryRepository(_context);
		public IRoomImageRepository roomImageRepository=>_roomImageRepository ?? new RoomImageRepository(_context);
		public ISelectedListRepository selectedListRepository => _selectedListRepository?? new SelectedListRepository(_context);
		public ISentQuestionRepository sentQuestionRepository =>_sentQuestionRepository?? new SentQuestionRepository(_context);
		public IServiceImageRepository serviceImageRepository =>_serviceImageRepository?? new ServiceImageRepository(_context);
		public IServiceOfferRepository serviceOfferRepository => _serviceOfferRepository?? new ServiceOfferRepository(_context);
		public ISettingTableRepository settingTableRepository => _settingTableRepository ?? new SettingsTableRepository(_context);
		public ISliderHomeRepository sliderHomeRepository =>_sliderHomeRepository?? new SliderHomeRepository(_context);
		public ITeamMemberRepository teamMemberRepository =>_teamMemberRepository?? new TeamMemberRepository(_context);	
		public ITeamMemberInfoRepository  teamMemberInfoRepository =>_teamMemberInfoRepository ?? new TeamMemberInfoRepository(_context);
		public IUserInfoRepository userInfoRepository =>_userInfoRepository?? new UserInfoRepository(_context);
		public IWhyUsRepository whyUsRepository =>_whyUsRepository ?? new WhyUsRepository(_context);

		public int Save()
		{
			return _context.SaveChanges();
		}
		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
