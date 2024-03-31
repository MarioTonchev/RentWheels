namespace RentWheels.Core.ViewModels.Car
{
	public class CarAllViewModel
	{
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string CarModel { get; set; } = string.Empty;
        public int Year { get; set; } 
        public string ImageUrl { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;
    }
}
