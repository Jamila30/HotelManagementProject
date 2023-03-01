namespace Hotel.Business.Mappers
{
	public class GallaryImageMapper:Profile
	{
		public GallaryImageMapper()
		{
			CreateMap<GallaryImage, GallaryImageDto>().ReverseMap();
		}
	}
}
