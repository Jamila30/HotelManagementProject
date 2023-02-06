using AutoMapper;
using Hotel.Business.DTOs.SliderHomeDTOs;
using Hotel.Core.Entities;

namespace Hotel.Business.Mappers
{
	public class SliderHomeMapper:Profile
	{
		public SliderHomeMapper()
		{
			CreateMap<SliderHomeDto,SliderHome>().ReverseMap();
			CreateMap<UpdateSliderHomeDto,SliderHome>().ReverseMap();
			CreateMap<CreateSliderHomeDto,SliderHome>().ReverseMap();
		}
	}
}
