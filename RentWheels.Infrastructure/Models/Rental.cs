using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RentWheels.Infrastructure.Constants.DataConstants.RentalConstants;

namespace RentWheels.Infrastructure.Models
{
    [Comment("Contains details about the renting of a car by a user")]
	public class Rental
	{
        [Key]
        [Comment("Identifier of the rental")]
		public int Id { get; set; }
        [Required]
        [Comment("Identifier of the rented car")]
        public int CarId { get; set; }
        [Required]
        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;
        [Required]
        [Comment("Identifier of the renter")]
        public string RenterId { get; set; } = string.Empty;
        [Required]
        [ForeignKey(nameof(RenterId))]
        public IdentityUser Renter { get; set; } = null!;
        [Required]
        [Comment("The date that the car was rented")]
        public DateTime Start { get; set; }
        [Required]
        [Comment("The date that the car rent will end")]
        public DateTime End { get; set; }
        [Required]
        [Comment("Total price of the rent")]
		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalPrice { get; set; }
        [Required]
        [StringLength(RentalMaxPickUpLocationLength)]
        [Comment("Where the car will be picked up")]
        public string PickUpLocation { get; set; } = string.Empty;
        [Required]
        [StringLength(RentalMaxDropOffLocationLength)]
        [Comment("Where the car will be dropped off")]
        public string DropOffLocation { get; set; } = string.Empty;
    }
}
