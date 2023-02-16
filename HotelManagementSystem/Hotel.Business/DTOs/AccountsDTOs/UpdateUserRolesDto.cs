namespace Hotel.Business.DTOs.AccountsDTOs
{
	public class UpdateUserRolesDto:IDto
	{
		public string? Email { get; set; }
		public string? OldRoleName { get; set; }
		public string? NewRoleName { get; set; }
		
	
	}
}
