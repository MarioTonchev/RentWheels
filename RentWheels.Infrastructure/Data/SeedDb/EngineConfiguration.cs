using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Infrastructure.Data.SeedDb
{
	internal class EngineConfiguration : IEntityTypeConfiguration<Engine>
	{
		public void Configure(EntityTypeBuilder<Engine> builder)
		{
			var data = new SeedData();

			builder.HasData(new Engine[] { data.SmallEngine, data.MediumEngine, data.BigEngine, data.SportEngine });
		}
	}
}
