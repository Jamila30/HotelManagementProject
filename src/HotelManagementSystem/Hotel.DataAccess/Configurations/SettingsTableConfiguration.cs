namespace Hotel.DataAccess.Configurations
{
	public class SettingsTableConfiguration : IEntityTypeConfiguration<SettingsTable>
	{
		public void Configure(EntityTypeBuilder<SettingsTable> builder)
		{
			builder.Property(x => x.Id).IsRequired();
			builder.Property(x => x.Key).IsRequired();
			builder.Property(x => x.Value).IsRequired();
		}
	}
}
