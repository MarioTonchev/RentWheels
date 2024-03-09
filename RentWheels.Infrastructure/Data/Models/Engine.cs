using System.ComponentModel.DataAnnotations;

namespace RentWheels.Infrastructure.Data.Models
{
	public class Engine
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public int Horsepower { get; set; }
        [Required]
        public int Cubage { get; set; }
        [Required]
        public string FuelType { get; set; } = string.Empty;
        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
    }
}