namespace Hotel.Business.Services.Interfaces
{
	public interface IUserInfoService
	{
		Task<List<UserInfoDto>> GetAllAsync();
		Task<List<UserInfoDto>> GetByCondition(Expression<Func<UserInfo, bool>> expression);
		Task<UserInfoDto?> GetByIdAsync(int id);
		Task<string> Create(CreateUserInfoDto entity);
		Task UpdateAsync(int id, UpdateUserInfoDto entity);
		Task Delete(int id);
		
	}
}
