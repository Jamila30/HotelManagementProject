using AutoMapper;
using Hotel.Business.DTOs.WhyUsDTOs;
using Hotel.Core.Entities;

namespace Hotel.Business.Mappers
{
	public class WhyUsMappper:Profile
	{
		public WhyUsMappper()
		{
			CreateMap<WhyUsDto,WhyUs>().ReverseMap();
			CreateMap<UpdateWhyUsDto,WhyUs>().ReverseMap();	
			CreateMap<CreateWhyUsDto,WhyUs>().ReverseMap();	
		}
	}
}
