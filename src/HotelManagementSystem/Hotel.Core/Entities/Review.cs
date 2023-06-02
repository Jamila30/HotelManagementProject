namespace Hotel.Core.Entities
{
	public class Review:IEntity
	{
		public int Id { get; set; }	
		public string? UserId { get; set; }
		public int FlatId { get; set; }
		public int Rate { get; set; }
		public string? Opinions { get; set; }

		//Naviation Property
		public Flat? Flat { get; set; }
		public AppUser? User { get; set; }
	}
}
