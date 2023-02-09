namespace Hotel.Core.Entities
{
	public class GallaryCatagory:IEntity
	{
		public int Id { get; set; }
		public string? Name { get; set; }

		public ICollection<GallaryImage>? Images { get; set; }
	}
}
