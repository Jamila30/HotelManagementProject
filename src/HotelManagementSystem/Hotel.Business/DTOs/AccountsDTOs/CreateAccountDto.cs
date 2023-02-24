namespace Hotel.Business.DTOs.AccountsDTOs
{
	public class CreateAccountDto
	{
		public string? UserName { get; set; }
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Password { get; set; }
		public string? ConfirmedPassword { get; set; }
	}
}
