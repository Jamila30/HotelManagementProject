namespace Hotel.Business.Mappers
{
	public class AmentityMapper:Profile
	{
		public AmentityMapper()
		{
			CreateMap<AmentityDto,Amentity>().ReverseMap();
			CreateMap<CreateAmentityDto,Amentity>().ReverseMap();
			CreateMap<UpdateAmentityDto,Amentity>().ReverseMap();

		}
	}
}
