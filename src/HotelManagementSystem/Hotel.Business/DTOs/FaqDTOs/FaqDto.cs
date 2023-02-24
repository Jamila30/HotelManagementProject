namespace Hotel.Business.DTOs.FaqDTOs
{
	public class FaqDto:IDto
	{
		public int Id { get; set; }
		public string? Question { get; set; }
		public string? Answer { get; set; }

	}
}
