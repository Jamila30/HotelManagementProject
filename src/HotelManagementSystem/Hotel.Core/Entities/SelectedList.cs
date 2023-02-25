namespace Hotel.Core.Entities
{
	public class SelectedList : IEntity
	{
		public int Id { get; set; }
		public int FlatId { get; set; }
		public int CatagoryId { get; set; }
		public string? CatagoryName { get; set; }
		public float Price { get; set; }

		//Navigation Property
		public Flat? Flat { get; set; }

	}
}
