namespace Hotel.Core.Entities
{
	public class Comment : IEntity
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Opinions { get; set; }
		public DateTime? Created { get; set; }
		public int FlatId { get; set; }
		//Navigation Property
		public Flat? Flat { get; set; }
	}
}
