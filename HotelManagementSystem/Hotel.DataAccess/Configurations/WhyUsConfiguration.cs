
namespace Hotel.DataAccess.Configurations
{
	public class WhyUsConfiguration : IEntityTypeConfiguration<WhyUs>
	{
		public void Configure(EntityTypeBuilder<WhyUs> builder)
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
