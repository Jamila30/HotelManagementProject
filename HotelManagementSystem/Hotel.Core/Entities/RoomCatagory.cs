namespace Hotel.Core.Entities
{
	public class RoomCatagory:IEntity
	{
		public RoomCatagory()
		{
			Flats=new HashSet<Flat>();
		}
		public int Id { get; set; }
		public string? Name { get; set; }

		//Naviagtion Property
		public ICollection<Flat>? Flats { get; set; }
		
	}
}
