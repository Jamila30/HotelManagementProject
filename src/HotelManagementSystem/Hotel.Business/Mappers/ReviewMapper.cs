using entity = Hotel.Core.Entities;
namespace Hotel.Business.Mappers
{
	public class ReviewMapper:Profile
	{
		public ReviewMapper()
		{
			CreateMap<entity.Review, ReviewDto>().ReverseMap();
		}
	}
}
