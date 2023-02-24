namespace Hotel.Business.DTOs.AuthorizationDTOs
{
	public class RegisterDto:IDto
	{
		
		public string? Username { get; set; }
		public string? Fullname { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? ConfirmedPassword { get; set; }
	}
}
