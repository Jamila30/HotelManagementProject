using Hotel.Business.DTOs.ServiceImageDTOs;

namespace Hotel.Business.Mappers
{
	public class ServiceImageMapper:Profile
	{
		public ServiceImageMapper()
		{
			CreateMap<ServiceImageDto,ServiceImage>().ReverseMap();
		}
	}
}
