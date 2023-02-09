using Microsoft.EntityFrameworkCore;

namespace Hotel.DataAccess.Configurations
{
	public class GallaryCatagoryConfiguration : IEntityTypeConfiguration<GallaryCatagory>
	{
		public void Configure(EntityTypeBuilder<GallaryCatagory> builder)
		{
			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(70);
		}
	}
}
