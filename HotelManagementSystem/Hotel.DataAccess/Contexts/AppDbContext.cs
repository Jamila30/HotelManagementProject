

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
		}

		public DbSet<SliderHome> SliderHomes { get; set; } = null!;
		public DbSet<WhyUs> WhyUss { get; set; } = null!;
		public DbSet<NearPlace> NearPlaces { get; set; } = null!;
		public DbSet<TeamMember> TeamMembers { get; set; } = null!;
		public DbSet<TeamMemberInformation> TeamMemberInformations { get; set; } = null!;
		public DbSet<ServiceImage> ServiceImages { get; set; }=null!;
		public DbSet<ServiceOffer> ServiceOffers { get; set; }= null!;

	}
}
