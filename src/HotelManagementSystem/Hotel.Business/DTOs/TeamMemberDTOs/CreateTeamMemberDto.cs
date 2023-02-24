

namespace Hotel.Business.DTOs.TeamMemberDTOs
{
	public class CreateTeamMemberDto:IDto
	{
	
		public string? Fullname { get; set; }
		public string? Position { get; set; }
		public IFormFile? Image { get; set; }
		

	}
}
