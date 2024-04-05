using RentWheels.Core.ViewModels.Rental;

namespace RentWheels.Core.ViewModels.Admin
{
	public class AdminAllRentalsViewModel : MyRentedCarsViewModel
	{
		public string UserEmail { get; set; } = string.Empty;
    }
}
