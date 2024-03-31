namespace RentWheels.Core.ViewModels.Car
{
	public class CarQueryViewModel
	{
		public int TotalCarsCount { get; set; }
		public int TotalPages { get; set; }

		public IEnumerable<CarAllViewModel> Cars { get; set; } = new List<CarAllViewModel>();
	}
}
