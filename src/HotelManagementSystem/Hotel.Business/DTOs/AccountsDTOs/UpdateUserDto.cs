namespace Hotel.Business.DTOs.AccountsDTOs
{
	public class UpdateUserDto:IDto
	{
		public string? userId { get; set; }
		public string? UserName { get; set; }
		public string? FullName { get; set; }
		public string? PhoneNumber { get; set; }
	}
}
