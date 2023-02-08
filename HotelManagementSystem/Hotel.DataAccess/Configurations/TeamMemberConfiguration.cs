

namespace Hotel.DataAccess.Configurations
{
	public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
	{
		public void Configure(EntityTypeBuilder<TeamMember> builder)
		{
			
			builder.Property(x => x.Position)
				.IsRequired()
				.HasMaxLength(70);
			builder.Property(x => x.Image)
				.IsRequired();
			builder.Property(x => x.Fullname)
				.IsRequired()
				.HasMaxLength(70);

		}
	}
}
