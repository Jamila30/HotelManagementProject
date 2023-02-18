namespace Hotel.Core.Entities
{
	public class Reservation:IEntity
	{
		public int Id { get; set; }
		public int FlatId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int Adults { get; set; }
		public int Children { get; set; }
		public string? PhoneNumber { get; set; }
		public string? FullName { get; set; }
		public float Price { get; set; }


	}
}
