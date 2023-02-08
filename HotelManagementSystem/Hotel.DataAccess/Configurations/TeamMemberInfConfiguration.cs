

namespace Hotel.DataAccess.Configurations
{
	public class TeamMemberInfConfiguration : IEntityTypeConfiguration<TeamMemberInformation>
	{
		public void Configure(EntityTypeBuilder<TeamMemberInformation> builder)
		{
			builder.Property(x => x.Twitter)
				.IsRequired()
				.HasMaxLength(120);
			builder.Property(x => x.Facebook)
				.IsRequired()
				.HasMaxLength(120);
			builder.Property(x => x.Instagram)
				.IsRequired()
				.HasMaxLength(120);
			builder.Property(x => x.Linkedin)
				.IsRequired()
				.HasMaxLength(120);
			builder.Property(x => x.Phone)
				.IsRequired()
				.HasMaxLength(70);


		}
	}
}
