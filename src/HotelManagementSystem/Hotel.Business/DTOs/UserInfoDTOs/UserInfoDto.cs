namespace Hotel.Business.DTOs.UserInfoDTOs
{
	public class UserInfoDto:IDto
	{
		public int Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? City { get; set; }
		public string? Country { get; set; }
		public string? Address { get; set; }
		public string? PostCode { get; set; }
		public string? Notes { get; set; }
		public string? UserId { get; set; }
	}
}
