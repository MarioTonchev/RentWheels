using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RentWheels.Infrastructure.Models
{
    [Comment("Details about the engine of the car")]
    public class Engine
    {
        [Key]
        [Comment("Identifier of the engine")]
        public int Id { get; set; }
        [Required]
        [Comment("Horsepower of the engine")]
        public int Horsepower { get; set; }
        [Required]
        [Comment("Cubage of the engine")]
        public int Cubage { get; set; }
        [Required]
        [Comment("Fuel type of the engine")]
        public string FuelType { get; set; } = string.Empty;
        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
    }
}