namespace Hotel.Business.DTOs.AuthorizationDTOs
{
	public class LoginDto:IDto
	{
		public string? Email { get; set; }
		public string? Password { get; set; }
	}
}
