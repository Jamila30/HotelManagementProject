namespace Hotel.Business.Mappers
{
	public class SelectedListMapper:Profile
	{
		public SelectedListMapper()
		{
			CreateMap <SelectedList,SelectedListDto>().ReverseMap();
		}
	}
}
