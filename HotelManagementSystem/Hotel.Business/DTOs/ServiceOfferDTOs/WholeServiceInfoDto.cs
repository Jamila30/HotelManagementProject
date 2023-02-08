namespace Hotel.Business.DTOs.ServiceOfferDTOs
{
	public class WholeServiceInfoDto:IDto
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public float? Price { get; set; }
		public bool IsFree { get; set; }
		public List<string>? Images=new List<string>();	
	}
}
