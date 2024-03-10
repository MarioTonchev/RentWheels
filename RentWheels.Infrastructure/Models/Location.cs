using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RentWheels.Infrastructure.Models
{
	[Comment("Information about where the car will be picked up and dropped off")]
	public class Location
	{
		[Key]
		[Comment("Identifier of the location")]
        public int Id { get; set; }
		[Required]
		[Comment("Where the car will be picked up by the renter")]
        public string PickUp { get; set; } = string.Empty;
		[Required]
		[Comment("Where the car will be dropped off by the renter")]
		public string DropOff { get; set; } = string.Empty;
		public IList<RentalLocation> RentalsLocations { get; set; } = new List<RentalLocation>();
	}
}
