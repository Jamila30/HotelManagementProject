

namespace Hotel.DataAccess.Repositories.Implementations
{
	public class TeamMemberRepository : Repository<TeamMember>, ITeamMemberRepository
	{
		public TeamMemberRepository(AppDbContext context) : base(context)
		{
		}
	}
}
