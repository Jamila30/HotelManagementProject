namespace Hotel.Business.DTOs.RoomImageDTOs
{
	public class UpdateRoomImageDto:IDto
	{
		public int Id { get; set; }
		public IFormFile? Image { get; set; }

		public int FlatId { get; set; }
	}
}
