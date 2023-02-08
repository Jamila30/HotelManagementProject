

namespace Hotel.DataAccess.Configurations
{
	public class ServiceOfferConfiguration : IEntityTypeConfiguration<ServiceOffer>
	{
		public void Configure(EntityTypeBuilder<ServiceOffer> builder)
		{
			builder.Property(x => x.Title)
				.IsRequired()
				.HasMaxLength(70);
			builder.Property(x => x.Description)
				.IsRequired();
			builder.Property(x=>x.Price) 
				.IsRequired();
			builder.Property(x => x.IsFree)
				.IsRequired();

		}
	}
}
