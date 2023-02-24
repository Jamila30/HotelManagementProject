namespace Hotel.Core.Entities
{
	public class FAQ:IEntity
	{
		public int Id { get; set; }
		public string? Question { get; set; }
		public string? Answer { get; set; }
	
	}
}
