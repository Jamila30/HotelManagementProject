namespace Hotel.Business.Mappers
{
	public class AccountMapper:Profile
	{
		public AccountMapper()
		{
			CreateMap<AppUser,AppUserDto>().ReverseMap();
			CreateMap<AppUser,CreateAccountDto>().ReverseMap();
		}
	}
}
