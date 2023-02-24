namespace Hotel.Business.Mappers
{
	public class TeamInfoMapper:Profile
	{
		public TeamInfoMapper()
		{
			CreateMap<TeamMemberInfoDto,TeamMemberInformation>().ReverseMap();
			CreateMap<TeamMemberInformation,CreateTeamInfoDto>().ReverseMap();
			CreateMap<TeamMemberInformation,UpdateTeamMemberInfoDto>().ReverseMap();
		}
	}
}
