

namespace Hotel.DataAccess.Configurations
{
	public class NearPlaceConfiguration : IEntityTypeConfiguration<NearPlace>
	{
		public void Configure(EntityTypeBuilder<NearPlace> builder)
		{
			builder.Property(x => x.Title)
				.IsRequired()
				.HasMaxLength(70);
			builder.Property(x => x.Description)
				.IsRequired()
				.HasMaxLength(250);
			builder.Property(x => x.Image)
				.IsRequired();

		}
	}
}
