namespace Hotel.DataAccess.Configurations
{
	public class RoomCatagoryConfiguration : IEntityTypeConfiguration<RoomCatagory>
	{
		public void Configure(EntityTypeBuilder<RoomCatagory> builder)
		{
			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(70);
		}
	}
}
