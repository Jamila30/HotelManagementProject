namespace Hotel.Business.DTOs.SelectedListDTOs
{
	public class SelectedListDto:IDto
	{
		public int Id { get; set; }
		public int FlatId { get; set; }
		public string? CatagoryName { get; set; }
		public float Price { get; set; }
		public Flat? Flat { get; set; }
	}
}
