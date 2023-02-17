namespace Hotel.DataAccess.Configurations
{
	public class FAQConfiguration : IEntityTypeConfiguration<FAQ>
	{
		public void Configure(EntityTypeBuilder<FAQ> builder)
		{
			builder.Property(x=>x.Id).IsRequired();
			builder.Property(x=>x.Answer).IsRequired();
			builder.Property(x=>x.Question).IsRequired();

		}
	}
}
