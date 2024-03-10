using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
			builder.Entity<RentalLocation>().HasKey(rl => new { rl.RentalId, rl.LocationId });

			base.OnModelCreating(builder);
		}

		public DbSet<Car> Cars { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<RentalLocation> RentalsLocations { get; set; }
    }
}