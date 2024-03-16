using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RentWheels.Infrastructure.Models
{
    [Comment("Shape of the car's coupe")]
    public class Category
    {
        [Key]
        [Comment("Identifier of category")]
        public int Id { get; set; }
        [Required]
        [Comment("Category's name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Comment("The number of doors this category has")]
        public int DoorCount { get; set; }
        public IList<Car> Cars { get; set; } = new List<Car>();
    }
}
