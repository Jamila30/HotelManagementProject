namespace Hotel.Core.Entities
{
	public class Flat : IEntity
	{
		public Flat()
		{
			Amentities=new HashSet<FlatAmentity>();
			Comments = new HashSet<Comment>();
			Images = new HashSet<RoomImage>();
			Reservations=new HashSet<Reservation>();
			SelectedLists=new HashSet<SelectedList>();
		}
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
		public ICollection<Comment>?  Comments { get; set; }
		public ICollection<FlatAmentity>? Amentities { get; set; }
		public ICollection<Reservation> Reservations { get; set; }
		public ICollection<SelectedList> SelectedLists{ get; set; }

	}
}
