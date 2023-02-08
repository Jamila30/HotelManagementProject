
namespace Hotel.Core.Entities
{
	public class TeamMember:IEntity
	{
		public int Id { get; set; }
		public string? Fullname { get; set; }
		public string? Position { get; set; }
		public string? Image { get; set; }
		


		//Navigation Property
		public TeamMemberInformation? TeamMemberInformation { get; set; }

	}
}
