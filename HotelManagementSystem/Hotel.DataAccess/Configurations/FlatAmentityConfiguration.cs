namespace Hotel.DataAccess.Configurations
{
	public class FlatAmentityConfiguration : IEntityTypeConfiguration<FlatAmentity>
	{
		public void Configure(EntityTypeBuilder<FlatAmentity> builder)
		{
			builder.Property(x => x.AmentityId).IsRequired();
			builder.Property(x => x.FlatId).IsRequired();
			builder.Property(x => x.Id).IsRequired();
		}
	}
}
