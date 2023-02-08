using Hotel.Business.DTOs.ServiceOfferDTOs;

namespace Hotel.Business.Mappers
{
	public class ServiceOfferMapper:Profile
	{
		public ServiceOfferMapper()
		{
			CreateMap<ServiceOffer,ServiceOfferDto>().ReverseMap();
			CreateMap<CreateServiceOfferDto,ServiceOffer>().ReverseMap();
			CreateMap<UpdateServiceOfferDto,ServiceOffer>().ReverseMap();
		}
	}
}
