namespace Hotel.DataAccess.Configurations
{
	public class ReservationConfigurations : IEntityTypeConfiguration<Reservation>
	{
		public void Configure(EntityTypeBuilder<Reservation> builder)
		{
			builder.Property(x => x.Id).IsRequired();
			builder.Property(x => x.FlatId).IsRequired();
			builder.Property(x=>x.StartDate).IsRequired();
			builder.Property(x=>x.EndDate).IsRequired();
			builder.Property(x=>x.UserId).IsRequired();
			builder.Property(x=>x.Adult).IsRequired();
			builder.Property(x=>x.Children).IsRequired();
			builder.Property(x=>x.IsCanceled).IsRequired();

		}
	}
}
