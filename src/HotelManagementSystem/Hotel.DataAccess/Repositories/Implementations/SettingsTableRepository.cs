namespace Hotel.DataAccess.Repositories.Implementations
{
	public class SettingsTableRepository : Repository<SettingsTable>, ISettingTableRepository
	{
		public SettingsTableRepository(AppDbContext context) : base(context)
		{
		}
	}
}
