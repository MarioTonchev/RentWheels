using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Comment("Identifier of the location (place of pick up and drop off)")]
        public int LocationId { get; set; }
        [Required]
        [ForeignKey(nameof(LocationId))]
        public Location Location { get; set; } = null!;
    }
}
