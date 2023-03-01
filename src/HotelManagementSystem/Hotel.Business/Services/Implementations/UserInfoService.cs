namespace Hotel.Business.Services.Implementations
{
	public class UserInfoService : IUserInfoService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;
		public UserInfoService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
		{
			_mapper = mapper;
			_userManager = userManager;
			_unitOfWork = unitOfWork;
		}


		public async Task<List<UserInfoDto>> GetAllAsync()
		{
			var list = await _unitOfWork.userInfoRepository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<UserInfoDto>>(list);
			return listDto;
		}

		public async Task<List<UserInfoDto>> GetByCondition(Expression<Func<UserInfo, bool>> expression)
		{
			var list = await _unitOfWork.userInfoRepository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<UserInfoDto>>(list);
			return listDto;
		}

		public async Task<UserInfoDto?> GetByIdAsync(int id)
		{
			var userInfo = await _unitOfWork.userInfoRepository.GetByIdAsync(id);
			var userDto = _mapper.Map<UserInfoDto>(userInfo);
			return userDto;
		}

		public async Task<string> Create(CreateUserInfoDto entity)
		{
			var user = await _userManager.FindByEmailAsync(entity.Email);
			if (user is null) throw new NotFoundException("there is no user with this email");
			var emailValue= await _unitOfWork.userInfoRepository.GetAll().FirstOrDefaultAsync(x => x.Email == entity.Email);
			if (emailValue != null) throw new AlreadyExistException("user already exist.Try to just change information");
			var userInfo = _mapper.Map<UserInfo>(entity);
			userInfo.UserId = user.Id;
			user.PhoneNumber = entity.Phone;
			await _unitOfWork.userInfoRepository.Create(userInfo);
			await _unitOfWork.SaveAsync();
			return userInfo.UserId;
		}
		public async Task UpdateAsync(int id, UpdateUserInfoDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("id didnt overlap");
			var userInfo = _unitOfWork.userInfoRepository.GetByCondition(x => x.Id == id);
			if (userInfo is null) throw new NotFoundException("there is no user info with this id");
			var updatedUser = _mapper.Map<UserInfo>(entity);
			var user = await _userManager.FindByEmailAsync(entity.Email);
			if (user is null) throw new NotFoundException("there is no user info with this email");
			updatedUser.UserId= user.Id;

			_unitOfWork.userInfoRepository.Update(updatedUser);
			await _unitOfWork.SaveAsync();
		}
		public async Task Delete(int id)
		{
			var userInfo = await _unitOfWork.userInfoRepository.GetByIdAsync(id);
			if (userInfo is null) throw new NotFoundException("there is no user info with this id");
			_unitOfWork.userInfoRepository.Delete(userInfo);
			await _unitOfWork.SaveAsync();
		}
	}
}
