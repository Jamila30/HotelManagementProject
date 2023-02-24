namespace Hotel.DataAccess.Repositories.Implementations
{
	public class UserInfoRepository : Repository<UserInfo>, IUserInfoRepository
	{
		public UserInfoRepository(AppDbContext context) : base(context)
		{
		}
	}
}
