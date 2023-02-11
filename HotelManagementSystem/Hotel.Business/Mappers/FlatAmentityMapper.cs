namespace Hotel.Business.Mappers
{
	public class FlatAmentityMapper:Profile
	{
		public FlatAmentityMapper()
		{
			CreateMap<FlatAmentityDto, FlatAmentity>().ReverseMap();
			CreateMap<UpdateFlatAmentityDto, FlatAmentity>().ReverseMap();
		

		}
	}
}
