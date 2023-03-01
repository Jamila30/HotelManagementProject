namespace Hotel.Business.Mappers
{
	public class RoomImageMapper:Profile
	{
		public RoomImageMapper()
		{
			CreateMap<RoomImageDto, RoomImage>().ReverseMap();
		}
	}
}
