namespace Hotel.Business.DTOs.SelectedListDTOs
{
	public class UpdateSelectedListDto:IDto
	{
		public int CatagoryId { get; set; }
		public List<int>? FlatIds { get; set; } = new List<int>();
	}
}
