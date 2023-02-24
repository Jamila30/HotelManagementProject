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
