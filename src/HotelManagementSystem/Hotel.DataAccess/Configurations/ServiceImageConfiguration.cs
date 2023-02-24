

namespace Hotel.DataAccess.Configurations
{
	public class ServiceImageConfiguration : IEntityTypeConfiguration<ServiceImage>
	{
		public void Configure(EntityTypeBuilder<ServiceImage> builder)
		{
			builder.Property(x => x.Image)
			      .IsRequired();

		}
	}
}
