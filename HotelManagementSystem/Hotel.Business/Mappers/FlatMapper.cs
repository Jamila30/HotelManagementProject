namespace Hotel.Business.Mappers
{
	public class FlatMapper:Profile
	{
		public FlatMapper()
		{
			CreateMap<FlatDto, Flat>().ReverseMap();
			CreateMap<CreateFlatDto, Flat>().ReverseMap();
			CreateMap<UpdateFlatDto, Flat>().ReverseMap();
		}
	}
}
