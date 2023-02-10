
namespace Hotel.DataAccess.Configurations
{
	public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.Property(c=>c.Name)
				.IsRequired()
				.HasMaxLength(70);
			builder.Property(c => c.Email)
				.IsRequired()
				.HasMaxLength(70);
			builder.Property(c => c.Opinions)
				.IsRequired();
			builder.Property(c => c.FlatId)
				.IsRequired();
			builder.Property(c => c.Created)
				.IsRequired();
			
	

		}
	}
}
