using Hotel.Core.Entities.Identity;

namespace Hotel.Core.Entities
{
	public class Reservation:IEntity
	{
		public int Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int FlatId { get; set; }
		public string? UserId { get; set; }
		public int Adult { get; set; }
		public int Children { get; set; }
		public float Price { get; set; }
		public bool IsCanceled { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsFinished { get; set; }

		//Navigation Property

		public Flat? Flat { get; set; }
		public AppUser? AppUser { get; set; }


	}
}
