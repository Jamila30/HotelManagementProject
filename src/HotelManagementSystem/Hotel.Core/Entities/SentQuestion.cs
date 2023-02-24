namespace Hotel.Core.Entities
{
	public class SentQuestion:IEntity
	{
		public int Id { get; set; }
		public string? Question { get; set; }	
		public string? Answer { get; set; }	
		public string? Email { get; set; }
		public bool? IsAnswered { get; set; }
        
	}
}
