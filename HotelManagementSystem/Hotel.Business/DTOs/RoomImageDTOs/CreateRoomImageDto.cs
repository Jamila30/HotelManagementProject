namespace Hotel.Business.DTOs.RoomImageDTOs
{
	public class CreateRoomImageDto:IDto
	{
		public IFormFile? Image { get; set; }

		public int FlatId { get; set; }
	}
}
