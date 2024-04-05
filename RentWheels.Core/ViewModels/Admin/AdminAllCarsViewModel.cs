using RentWheels.Core.ViewModels.Car;

namespace RentWheels.Core.ViewModels.Admin
{
	public class AdminAllCarsViewModel : CarAllViewModel
	{
        public string UserEmail { get; set; } = string.Empty;
    }
}
