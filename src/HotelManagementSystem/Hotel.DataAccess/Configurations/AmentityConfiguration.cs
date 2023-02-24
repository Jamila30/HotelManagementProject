namespace Hotel.DataAccess.Configurations
{
	public class AmentityConfiguration : IEntityTypeConfiguration<Amentity>
	{
		public void Configure(EntityTypeBuilder<Amentity> builder)
		{
			builder.Property(x => x.Image)
				.IsRequired();
			builder.Property(x => x.Description)
				.IsRequired();
			builder.Property(x => x.Title)
				.IsRequired()
				.HasMaxLength(70);

		}
	}
}
