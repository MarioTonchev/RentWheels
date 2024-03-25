namespace RentWheels.Core.VeiwModels.Car
{
	public class CarDetailsViewModel
	{
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string CarModel { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public string Available { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int EngineId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
