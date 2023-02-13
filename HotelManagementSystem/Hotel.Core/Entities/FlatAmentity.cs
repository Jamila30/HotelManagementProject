using System.ComponentModel.DataAnnotations;

namespace Hotel.Core.Entities
{
	public class FlatAmentity:IEntity
	{
		
		public int Id { get; set; }
		public int FlatId { get; set; }
		public int AmentityId { get; set; }
		//Naviagtion Properties
		public Flat? Flat { get; set; }
		public Amentity? Amentity { get; set; }
		
	}
}
