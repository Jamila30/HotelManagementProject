namespace Hotel.DataAccess.Configurations
{
	public class RoomImageConfiguration : IEntityTypeConfiguration<RoomImage>
	{
		public void Configure(EntityTypeBuilder<RoomImage> builder)
		{
			builder.Property(x => x.Id)
				.IsRequired();
			builder.Property(x => x.Image)
				.IsRequired();
			builder.Property(x => x.FlatId)
				.IsRequired();
		}
	}
}
