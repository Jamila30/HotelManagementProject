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
