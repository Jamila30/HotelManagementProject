namespace Hotel.Business.DTOs.ReviewDTOs
{
	public class UpdateReviewDto:IDto
	{
		public int Id { get; set; }
		public int Rate { get; set; }
		public string? Opinions { get; set; }
	}
}
