
namespace Hotel.Business.DTOs.TeamMemberInfoDTOs
{
	public class CreateWholeInfoDto:IDto
	{
		public string? Phone { get; set; }
		public string? Facebook { get; set; }
		public string? Instagram { get; set; }
		public string? Twitter { get; set; }
		public string? Linkedin { get; set; }

		public string? MemberFullname { get; set; }
		public string? MemberPosition { get; set; }
		public IFormFile? MemberImage { get; set; }

	}
}
