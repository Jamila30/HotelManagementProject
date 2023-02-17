using Hotel.Core.Entities.Identity;

namespace Hotel.Core.Entities
{
	public class Comment : IEntity
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Opinions { get; set; }
		public DateTime? Created { get; set; }
		public int FlatId { get; set; }
		public string? UserId { get; set; }
		//Navigation Property
		public Flat? Flat { get; set; }
		public AppUser? User { get; set; }
	}
}
