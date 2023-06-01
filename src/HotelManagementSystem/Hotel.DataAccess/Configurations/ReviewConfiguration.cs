namespace Hotel.DataAccess.Configurations
{
	public class ReviewConfiguration : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			builder.Property(r => r.Id).IsRequired();
			builder.Property(r=>r.UserId).IsRequired();
			builder.Property(r=>r.FlatId).IsRequired();
			builder.Property(r=>r.Rate).IsRequired();
			builder.Property(r=>r.Opinions).HasMaxLength(300).IsRequired();
		}
	}
}
