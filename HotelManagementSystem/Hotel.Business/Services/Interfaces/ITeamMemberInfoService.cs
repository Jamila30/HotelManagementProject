namespace Hotel.Business.Services.Interfaces
{
	public interface ITeamMemberInfoService
	{
		Task<List<TeamMemberInfoDto>> GetAllAsync();
		Task<List<TeamMemberInfoDto>> GetByCondition(Expression<Func<TeamMemberInformation, bool>> expression);
		Task<TeamMemberInfoDto?> GetByIdAsync(int id);
		Task CreateInfoForExistMember(int id, CreateTeamInfoDto entity);
		Task CreateInfoWithNewMember(CreateWholeInfoDto entity);
		Task UpdateAsync(int id, UpdateTeamMemberInfoDto entity);
		Task Delete(int id);
	}
}
