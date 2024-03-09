using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RentWheels.Infrastructure.Constants.DataConstants;

namespace RentWheels.Infrastructure.Data.Models
{
	public class Car
	{
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(CarMaxBrandLength, MinimumLength = CarMinBrandLength)]
        public string Brand { get; set; } = string.Empty;
        [Required]
        [StringLength(CarMaxModelLength, MinimumLength = CarMinBrandLength)]
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        [Required]
        [StringLength(CarMaxColorLength, MinimumLength = CarMinColorLength)]
        public string Color { get; set; } = string.Empty;
        [Required]
        public string RegistrationNumber { get; set; } = string.Empty;
        [Required]
        [Range(CarMinPricePerDay, CarMaxPricePerDay)]
        public decimal PricePerDay { get; set; }
        [Required]
        [Range(0, 1)]
        public int Availability { get; set; }
        [Required]
        public int EngineId { get; set; }
        [Required]
        [ForeignKey(nameof(EngineId))]
        public Engine Engine { get; set; } = null!;
    }
}