﻿

namespace Hotel.DataAccess.Contexts
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(SliderHomeConfiguration).Assembly);
			base.OnModelCreating(modelBuilder);

			#region 1-1 relationship between TeamMember and TeamMemberInformation
			modelBuilder.Entity<TeamMember>()
				   .HasOne(m => m.TeamMemberInformation)
				   .WithOne(m => m.TeamMember)
				   .HasForeignKey<TeamMemberInformation>(m => m.Id);
			modelBuilder.Entity<TeamMemberInformation>()
				.HasKey(c => c.Id);

			#endregion

			#region 1-to-many relationship between ServiceOffer and ServiceImage 
			modelBuilder.Entity<ServiceImage>()
			  .HasOne(x => x.ServiceOffer)
			  .WithMany(x => x.ServiceImages)
			  .HasForeignKey(x => x.ServiceOfferId);
			#endregion

			#region 1-to-many relationship between  GallaryImage and GallaryCatagory
			modelBuilder.Entity<GallaryImage>()
				.HasOne(x => x.GallaryCatagory)
				.WithMany(x => x.Images)
				.HasForeignKey(x => x.GallaryCatagoryId);
			#endregion

			#region 1-to-many relationship between Flat and RoomImage
			modelBuilder.Entity<RoomImage>()
				.HasOne(r => r.Flat)
				.WithMany(f => f.Images)
				.HasForeignKey(r => r.FlatId);
			#endregion

			#region 1-to-many relationship between Flat and RoomCatagory
			modelBuilder.Entity<Flat>()
				.HasOne(f => f.RoomCatagory)
				.WithMany(c => c.Flats)
				.HasForeignKey(f => f.RoomCatagoryId);
			#endregion
		}

		public DbSet<SliderHome> SliderHomes { get; set; } = null!;
		public DbSet<WhyUs> WhyUss { get; set; } = null!;
		public DbSet<NearPlace> NearPlaces { get; set; } = null!;
		public DbSet<TeamMember> TeamMembers { get; set; } = null!;
		public DbSet<TeamMemberInformation> TeamMemberInformations { get; set; } = null!;
		public DbSet<ServiceImage> ServiceImages { get; set; } = null!;
		public DbSet<ServiceOffer> ServiceOffers { get; set; } = null!;
		public DbSet<GallaryImage> GallaryImages { get; set; } = null!;
		public DbSet<GallaryCatagory> GallaryCatagories { get; set; } = null!;
		public DbSet<Flat> Flats { get; set; } = null!;
		public DbSet<RoomImage> RoomImages { get; set; } = null!;
		public DbSet<RoomCatagory> RoomCatagories { get; set; } =null!;


	}
}
