namespace Hotel.Core.Entities
{
	public class ServiceOffer:IEntity
	{
		
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public float? Price { get; set; }
		public bool IsFree { get; set; }

	
		//Naviagtion Property
		public ICollection<ServiceImage>? ServiceImages { get; set; }
		
	}
}
