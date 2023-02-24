namespace Hotel.Core.Entities
{
	public class RoomImage:IEntity
	{
		public int Id { get; set; }
		public string? Image { get; set; }

		public int FlatId { get; set; }
		public Flat? Flat { get; set; }
	}
}
