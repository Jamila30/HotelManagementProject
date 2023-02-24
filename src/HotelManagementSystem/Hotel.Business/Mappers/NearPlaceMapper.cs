namespace Hotel.Business.Mappers
{
	public class NearPlaceMapper:Profile
	{
		public NearPlaceMapper()
		{
			CreateMap<NearPlaceDto,NearPlace>().ReverseMap();
			CreateMap<CreateNearPlaceDto,NearPlace>().ReverseMap();
			CreateMap<UpdateNearPlaceDto,NearPlace>().ReverseMap();
		}
	}
}
