

namespace Hotel.DataAccess.Configurations
{
	public class SliderHomeConfiguration : IEntityTypeConfiguration<SliderHome>
	{
		public void Configure(EntityTypeBuilder<SliderHome> builder)
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
