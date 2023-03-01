using Hotel.Business.DTOs.UserInfoDTOs;

namespace Hotel.Business.Services.Implementations
{
	public class UserInfoService : IUserInfoService
	{
		private readonly IUserInfoRepository _repository;
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;
		public UserInfoService(IUserInfoRepository repository, IMapper mapper, UserManager<AppUser> userManager)
		{
			_repository = repository;
			_mapper = mapper;
			_userManager = userManager;
		}


		public async Task<List<UserInfoDto>> GetAllAsync()
		{
			var list = await _repository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<UserInfoDto>>(list);
			return listDto;
		}

		public async Task<List<UserInfoDto>> GetByCondition(Expression<Func<UserInfo, bool>> expression)
		{
			var list = await _repository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<UserInfoDto>>(list);
			return listDto;
		}

		public async Task<UserInfoDto?> GetByIdAsync(int id)
		{
			var userInfo = await _repository.GetByIdAsync(id);
			var userDto = _mapper.Map<UserInfoDto>(userInfo);
			return userDto;
		}

		public async Task<string> Create(CreateUserInfoDto entity)
		{
			var user = await _userManager.FindByEmailAsync(entity.Email);
			if (user is null) throw new NotFoundException("there is no user with this email");
			var emailValue= await _repository.GetAll().FirstOrDefaultAsync(x => x.Email == entity.Email);
			if (emailValue != null) throw new AlreadyExistException("user already exist.Try to just change information");
			var userInfo = _mapper.Map<UserInfo>(entity);
			userInfo.UserId = user.Id;
			user.PhoneNumber = entity.Phone;
			await _repository.Create(userInfo);
			await _repository.SaveChanges();
			return userInfo.UserId;
		}
		public async Task UpdateAsync(int id, UpdateUserInfoDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("id didnt overlap");
			var userInfo = _repository.GetByCondition(x => x.Id == id);
			if (userInfo is null) throw new NotFoundException("there is no user info with this id");
			var updatedUser = _mapper.Map<UserInfo>(entity);
			var user = await _userManager.FindByEmailAsync(entity.Email);
			if (user is null) throw new NotFoundException("there is no user info with this email");
			updatedUser.UserId= user.Id;

			_repository.Update(updatedUser);
			await _repository.SaveChanges();
		}
		public async Task Delete(int id)
		{
			var userInfo = await _repository.GetByIdAsync(id);
			if (userInfo is null) throw new NotFoundException("there is no user info with this id");
			_repository.Delete(userInfo);
			await _repository.SaveChanges();
		}
	}
}
