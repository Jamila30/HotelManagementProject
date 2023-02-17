namespace Hotel.DataAccess.Configurations
{
	public class SentQuestionConfiguration : IEntityTypeConfiguration<SentQuestion>
	{
		public void Configure(EntityTypeBuilder<SentQuestion> builder)
		{
			builder.Property(x => x.Id).IsRequired();
			builder.Property(x => x.Question).IsRequired();
			builder.Property(x => x.Email).IsRequired();
			builder.Property(x => x.IsAnswered).IsRequired();
			
		}
	}
}
