

namespace Hotel.Core.Entities
{
	public class ServiceImage:IEntity
	{
		public int Id { get; set; }
		public string? Image { get; set; }
		public int ServiceOfferId { get; set; }

		//Navigation Property
		public ServiceOffer? ServiceOffer { get; set; }
	}
}
