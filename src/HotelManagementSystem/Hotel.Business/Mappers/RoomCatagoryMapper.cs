namespace Hotel.Business.Mappers
{
	public class RoomCatagoryMapper:Profile
	{
		public RoomCatagoryMapper()
		{
			CreateMap<RoomCatagoryDto,RoomCatagory>().ReverseMap();
		}
	}
}
