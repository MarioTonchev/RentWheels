using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentWheels.Infrastructure.Data.SeedDb;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new EngineConfiguration());
			builder.ApplyConfiguration(new CarConfiguration());

			base.OnModelCreating(builder);
		}

		public DbSet<Car> Cars { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}