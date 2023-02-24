using Hotel.Business.DTOs.UserInfoDTOs;

namespace Hotel.Business.Mappers
{
	public class UserInfoMapper:Profile
	{
		public UserInfoMapper()
		{
			CreateMap<UserInfo, UserInfoDto>().ReverseMap();
			CreateMap<UserInfo, CreateUserInfoDto>().ReverseMap();
			CreateMap<UserInfo, UpdateUserInfoDto>().ReverseMap();
		}
	}
}
