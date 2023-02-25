namespace Hotel.DataAccess.Configurations
{
	public class SelectedListConfiguration : IEntityTypeConfiguration<SelectedList>
	{
		public void Configure(EntityTypeBuilder<SelectedList> builder)
		{
			builder.Property(s => s.Price).IsRequired();
			builder.Property(s => s.Id).IsRequired();
			builder.Property(s => s.CatagoryName).IsRequired();
			builder.Property(s => s.FlatId).IsRequired();
			builder.Property(s => s.CatagoryId).IsRequired();
		}
	}
}
