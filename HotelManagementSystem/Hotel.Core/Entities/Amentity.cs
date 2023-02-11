namespace Hotel.Core.Entities
{
	public class Amentity:IEntity
	{
		public Amentity() 
		{
			this.Flats=new HashSet<FlatAmentity>();
		}
		public int Id { get; set; }
		public string? Image { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		//Navigation Property
		public ICollection<FlatAmentity>? Flats { get; set; }

	}
}
