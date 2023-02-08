namespace Hotel.Business.Services.Interfaces
{
	public interface ITeamMemberService
	{
		Task<List<TeamMemberDto>> GetAllAsync();
		Task<List<TeamMemberDto>> GetByCondition(Expression<Func<TeamMember, bool>> expression);
		Task<OneMemberInfoDto?> GetByIdAsync(int id);
		Task CreateTeamWithInfo(CreateWholeMemberDto createWholeMember);
		Task Create(CreateTeamMemberDto createTeam);
		Task UpdateAsync(int id, UpdateTeamMemberDto entity);
		Task Delete(int id);
	}
}
