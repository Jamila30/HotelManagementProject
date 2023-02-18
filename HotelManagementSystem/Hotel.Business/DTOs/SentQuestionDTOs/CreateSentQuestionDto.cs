namespace Hotel.Business.DTOs.SentQuestionDTOs
{
	public class CreateSentQuestionDto:IDto
	{
		public string? Question { get; set; }
		public string? Email { get; set; }
	}
}
