namespace Hotel.Core.Entities
{
	public class FlatAmentity:IEntity
	{
		public int Id { get ; set ; }
		public int FlatId { get; set; }
		public Flat? Flat { get; set; }
		public int AmentityId { get; set; }
		public Amentity? Amentity { get; set; }
	}
}
