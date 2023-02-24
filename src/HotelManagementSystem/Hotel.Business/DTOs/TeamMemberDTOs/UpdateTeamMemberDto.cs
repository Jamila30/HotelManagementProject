
namespace Hotel.Business.DTOs.TeamMemberDTOs
{
	public class UpdateTeamMemberDto:IDto
	{
		public int Id { get; set; }
		public string? Fullname { get; set; }
		public string? Position { get; set; }
		public IFormFile? Image { get; set; }
		

	}
}
