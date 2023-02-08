

namespace Hotel.Business.DTOs.TeamMemberDTOs
{
	public class TeamMemberDto:IDto
	{
		public int Id { get; set; }
		public string? Fullname { get; set; }
		public string? Position { get; set; }
		public string? Image { get; set; }
		

	}
}
