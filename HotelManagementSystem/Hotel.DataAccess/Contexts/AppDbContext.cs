using Hotel.Core.Entities;
using Hotel.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Contexts
{
	public class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options ):base(options) 
		{
				
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(SliderHomeConfiguration).Assembly);
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<SliderHome> SliderHomes { get; set; } = null!;
		public DbSet<WhyUs> WhyUs { get; set; } = null!;
	}
}
