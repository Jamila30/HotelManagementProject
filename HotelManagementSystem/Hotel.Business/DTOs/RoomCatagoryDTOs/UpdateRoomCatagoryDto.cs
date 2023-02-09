namespace Hotel.Business.DTOs.RoomCatagoryDTOs
{
	public class UpdateRoomCatagoryDto:IDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }
	}
}
