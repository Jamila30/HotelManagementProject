
namespace Hotel.DataAccess.Repositories.Implementations
{
	public class TeamMemberInfoRepository : Repository<TeamMemberInformation>, ITeamMemberInfoRepository
	{
		public TeamMemberInfoRepository(AppDbContext context) : base(context)
		{
		}
	}
}
