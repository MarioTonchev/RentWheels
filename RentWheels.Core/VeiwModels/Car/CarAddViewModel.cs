namespace RentWheels.Core.VeiwModels.Car
{
	public class CarAddViewModel
	{
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Year { get; set; }
        public int EngineId { get; set; }
        public decimal PricePerDay { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
