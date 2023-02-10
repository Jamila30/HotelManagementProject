namespace Hotel.Business.DTOs.RoomImageDTOs
{
	public class RoomImageDto:IDto
	{
		public int Id { get; set; }
		public string? Image { get; set; }

		public int FlatId { get; set; }
	}
}
