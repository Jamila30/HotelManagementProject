namespace Hotel.DataAccess.Configurations
{
	public class FlatConfiguration : IEntityTypeConfiguration<Flat>
	{
		public void Configure(EntityTypeBuilder<Flat> builder)
		{
			builder.Property(x => x.Id)
				.IsRequired();
			builder.Property(x => x.BedCount)
				.IsRequired();
			builder.Property(x => x.Price)
				.IsRequired();
			builder.Property(x => x.Size)
				.IsRequired();
			builder.Property(x => x.RoomCatagoryId)
				.IsRequired();
			builder.Property(x => x.Description)
				.IsRequired();
			builder.Property(x => x.DiscountPercent)
				.IsRequired();
			builder.Property(x => x.DiscountPrice)
				.IsRequired();
			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(70);
		}
	}
}
