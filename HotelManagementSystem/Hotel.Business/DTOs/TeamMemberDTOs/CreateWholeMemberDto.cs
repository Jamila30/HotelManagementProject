

namespace Hotel.Business.DTOs.TeamMemberDTOs
{
	public class CreateWholeMemberDto:IDto
	{
		public string? Fullname { get; set; }
		public string? Position { get; set; }
		public IFormFile? Image { get; set; }

		public string? Phone { get; set; }
		public string? Facebook { get; set; }
		public string? Instagram { get; set; }
		public string? Twitter { get; set; }
		public string? Linkedin { get; set; }

	}
}
