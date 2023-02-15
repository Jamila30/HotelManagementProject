namespace Hotel.Business.DTOs.AuthorizationDTOs
{
	public class ResetPasswordDto:IDto
	{
		public string? NewPassword { get; set; }
		public string? NewConfirmedPassword { get; set; }

	}
}
