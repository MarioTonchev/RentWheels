using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RentWheels.Infrastructure.Constants.DataConstants.CarConstants;

namespace RentWheels.Infrastructure.Models
{
    [Comment("Information about the car that can be rented")]
    public class Car
    {
        [Key]
        [Comment("Identifier for car")]
        public int Id { get; set; }
        [Required]
        [StringLength(CarMaxBrandLength)]
        [Comment("Brand of the car")]
        public string Brand { get; set; } = string.Empty;
        [Required]
        [StringLength(CarMaxModelLength)]
        [Comment("Model of the car")]
        public string Model { get; set; } = string.Empty;
        [Required]
        [Comment("Year in which the car was produced")]
        public int Year { get; set; }
        [Required]
        [StringLength(CarMaxColorLength)]
        [Comment("Color of the car")]
        public string Color { get; set; } = string.Empty;
        [Required]
        [Comment("Price per day for renting the car")]
        public decimal PricePerDay { get; set; }
        [Required]
        [Comment("Checks wheter the car is rented or not")]
        public string Available { get; set; } = string.Empty;
        [Required]
        [Comment("Id of the engine of the car")]
        public int EngineId { get; set; }
        [Required]
        [ForeignKey(nameof(EngineId))]
        public Engine Engine { get; set; } = null!;
    }
}