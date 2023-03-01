namespace Hotel.Business.DTOs.SettingTableDTOs
{
	public class UpdateKeyDto:IDto
	{
		public string? OldKey { get; set; }
		public string? NewKey { get; set;}
	}
}
