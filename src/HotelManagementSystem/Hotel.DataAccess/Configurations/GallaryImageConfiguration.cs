namespace Hotel.DataAccess.Configurations
{
	public class GallaryImageConfiguration : IEntityTypeConfiguration<GallaryImage>
	{
		public void Configure(EntityTypeBuilder<GallaryImage> builder)
		{
			builder.Property(x => x.Image)
				.IsRequired();
			builder.Property(x => x.GallaryCatagoryId)
				.IsRequired();
		}
	}
}
