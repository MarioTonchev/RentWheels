using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static RentWheels.Infrastructure.Constants.DataConstants.EngineConstants;

namespace RentWheels.Infrastructure.Models
{
    [Comment("Details about the engine of the car")]
    public class Engine
    {
        [Key]
        [Comment("Identifier of the engine")]
        public int Id { get; set; }
        [Required]
        [StringLength(EngineMaxNameLength)]
        [Comment("Name of the engine")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Comment("Horsepower of the engine")]
        public int Horsepower { get; set; }
        [Required]
        [Comment("Cubage of the engine")]
        public int Cubage { get; set; }
        [Required]
        [StringLength(EngineMaxFuelTypeLength)]
        [Comment("Fuel type of the engine")]
        public string FuelType { get; set; } = string.Empty;
        public IList<Car> Cars { get; set; } = new List<Car>();
    }
}