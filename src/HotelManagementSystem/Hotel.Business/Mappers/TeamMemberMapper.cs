namespace Hotel.Business.Mappers
{
	public class TeamMemberMapper:Profile
	{
		public TeamMemberMapper()
		{
			CreateMap<TeamMemberDto,TeamMember>().ReverseMap();
			CreateMap<CreateTeamMemberDto,TeamMember>().ReverseMap();
			CreateMap<UpdateTeamMemberDto,TeamMember>().ReverseMap();
		}
	}
}
