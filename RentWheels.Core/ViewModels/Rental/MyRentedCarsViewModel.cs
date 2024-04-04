namespace RentWheels.Core.ViewModels.Rental
{
    public class MyRentedCarsViewModel
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string CarModel { get; set; } = string.Empty;
        public string Start { get; set; } = string.Empty;
        public string End { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string PickUp { get; set; } = string.Empty;
        public string DropOff { get; set; } = string.Empty;
    }
}
