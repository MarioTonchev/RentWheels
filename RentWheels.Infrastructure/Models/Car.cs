using Microsoft.AspNetCore.Identity;
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
		[Column(TypeName = "decimal(18,2)")]
		public decimal PricePerDay { get; set; }
        [Required]
        [Comment("Url of the image of the car")]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        [Comment("Checks whether the car is rented or not")]
        public string Available { get; set; } = string.Empty;
        [Required]
        [Comment("Id of the engine of the car")]
        public int EngineId { get; set; }
        [Required]
        [ForeignKey(nameof(EngineId))]
        public Engine Engine { get; set; } = null!;
        [Required]
        [Comment("Identifier of the owner of the car")]
        public string OwnerId { get; set; } = string.Empty;
        [Required]
        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;
	}
}