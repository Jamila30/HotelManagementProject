namespace Hotel.Business.DTOs.AccountsDTOs
{
	public class UserRoleDto:IDto
	{
		public string? Email { get; set; }
		public string? RoleName { get; set; }
	}
}
