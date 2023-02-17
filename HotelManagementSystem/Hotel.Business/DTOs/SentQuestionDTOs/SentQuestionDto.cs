namespace Hotel.Business.DTOs.SentQuestionDTOs
{
	public class SentQuestionDto:IDto
	{
		public int Id { get; set; }
		public string? Question { get; set; }
		public string? Email { get; set; }
		public bool? IsAnswered { get; set; }
	}
}
