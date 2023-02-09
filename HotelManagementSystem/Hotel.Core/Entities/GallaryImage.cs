namespace Hotel.Core.Entities
{
	public class GallaryImage : IEntity
	{
		public int Id { get; set; }
		public string? Image { get; set; }
		public int GallaryCatagoryId { get; set; }
		//Navigation Property
		public GallaryCatagory? GallaryCatagory { get; set; }
	}
}
