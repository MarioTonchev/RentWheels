using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Infrastructure.Data.SeedDb
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            var data = new SeedData();

            builder.HasData(new Category[] { data.Sedan, data.Hatchback, data.SUV, data.Estate, data.Coupe, data.Supercar });
        }
    }
}
