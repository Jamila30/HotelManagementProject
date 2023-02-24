namespace Hotel.Core.Entities
{
	public class FlatAmentity
	{
		public int FlatId { get; set; }
		public int AmentityId { get; set; }
		//Naviagtion Properties
		public Flat? Flat { get; set; }
		public Amentity? Amentity { get; set; }
		
	}
}
