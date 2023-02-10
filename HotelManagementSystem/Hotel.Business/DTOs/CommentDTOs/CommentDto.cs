namespace Hotel.Business.DTOs.CommentDTOs
{
	public class CommentDto:IDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Opinions { get; set; }
		public DateTime? Created { get; set; }
		public int FlatId { get; set; }
	}
}
