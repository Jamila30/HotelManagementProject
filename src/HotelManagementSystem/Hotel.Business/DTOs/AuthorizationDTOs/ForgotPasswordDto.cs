namespace Hotel.Business.DTOs.AuthorizationDTOs
{
	public class ForgotPasswordDto:IDto
	{
		public string? EmailOrUsername { get; set; }
	}
}
