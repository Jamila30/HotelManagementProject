using Hotel.Business.DTOs.SelectedListDTOs;

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
