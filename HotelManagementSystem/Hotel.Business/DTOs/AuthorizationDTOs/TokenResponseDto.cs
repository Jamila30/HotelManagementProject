namespace Hotel.Business.DTOs.AuthorizationDTOs
{
	public class TokenResponseDto:IDto
	{
		public string? Token { get; set; }
		public DateTime? ExpireDate { get; set; }

		public string? Username { get; set;}

	}
}
