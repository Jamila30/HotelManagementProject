namespace Hotel.Business.DTOs.RoleManageDTOs
{
	public class UpdateRoleDto:IDto
	{
		public string? OldRoleName { get; set;}
		public string? NewRoleName { get; set;}
	}
}
