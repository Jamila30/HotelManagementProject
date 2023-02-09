namespace Hotel.Core.Entities
{
	public class Flat : IEntity
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public float Price { get; set; }
		public int Adults { get; set; }
		public int Children { get; set; }
		public int Size { get; set; }
		public int BedCount { get; set; }
		public string? Description { get; set; }
		public int RoomCatagoryId { get; set; }

		//Navigation property
		public RoomCatagory? RoomCatagory { get; set; }
		public ICollection<RoomImage>? Images { get; set; }
	}
}
