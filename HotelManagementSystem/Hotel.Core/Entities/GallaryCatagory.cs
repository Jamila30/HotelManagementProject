namespace Hotel.Core.Entities
{
	public class GallaryCatagory:IEntity
	{
		public GallaryCatagory()
		{
			Images=new HashSet<GallaryImage>();
		}
		public int Id { get; set; }
		public string? Name { get; set; }

		public ICollection<GallaryImage>? Images { get; set; }
	}
}
