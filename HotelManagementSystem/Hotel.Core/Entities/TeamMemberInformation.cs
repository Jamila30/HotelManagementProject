
namespace Hotel.Core.Entities
{
	public class TeamMemberInformation:IEntity
	{
		public int Id { get; set; }
		public string? Phone { get; set; }
		public string? Facebook { get; set; }
		public string? Instagram { get; set; }
		public string? Twitter { get; set; }
		public string? Linkedin { get; set; }

		//Navigation Property
		public TeamMember? TeamMember { get; set; }
	}
}
