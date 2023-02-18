namespace Hotel.DataAccess.Configurations
{
	public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
	{
		public void Configure(EntityTypeBuilder<UserInfo> builder)
		{
			builder.Property(x => x.Address).IsRequired().HasMaxLength(120);
			builder.Property(x => x.PostCode).IsRequired().HasMaxLength(70);
			builder.Property(x => x.City).IsRequired().HasMaxLength(70);
			builder.Property(x => x.Country).IsRequired().HasMaxLength(70);
			builder.Property(x => x.Email).IsRequired().HasMaxLength(120);
			builder.Property(x => x.LastName).IsRequired().HasMaxLength(70);
			builder.Property(x => x.FirstName).IsRequired().HasMaxLength(70);
			builder.Property(x => x.Phone).IsRequired().HasMaxLength(70);
			builder.Property(x => x.Id).IsRequired();
			builder.Property(x => x.UserId).IsRequired();
		}
	}
}
