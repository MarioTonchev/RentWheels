using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Infrastructure.Data.SeedDb
{
	internal class CarConfiguration : IEntityTypeConfiguration<Car>
	{
		public void Configure(EntityTypeBuilder<Car> builder)
		{
			var data = new SeedData();

			builder.HasData(new Car[] { data.FirstCar, data.SecondCar });
		}
	}
}
