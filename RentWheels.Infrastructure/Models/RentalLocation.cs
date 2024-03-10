using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentWheels.Infrastructure.Models
{
	public class RentalLocation
	{
        [Required]
        [Comment("Identifier of the rental, part of composite key")]
        public int RentalId { get; set; }
        [Required]
        [ForeignKey(nameof(RentalId))]
        public Rental Rental { get; set; } = null!;
        [Required]
        [Comment("Identifier of the location, part of composite key")]
        public int LocationId { get; set; }
        [Required]
        [ForeignKey(nameof(LocationId))]
        public Location Location { get; set; } = null!;
    }
}
