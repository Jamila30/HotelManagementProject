namespace Hotel.Business.DTOs.ReviewDTOs
{
	public class CreateReviewDto:IDto
	{
		public string? UserId { get; set; }
		public int FlatId { get; set; }
		public int Rate { get; set; }
		public string? Opinions { get; set; }
	}
}
