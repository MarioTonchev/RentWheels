using RentWheels.Core.ViewModels.Admin;
using RentWheels.Core.ViewModels.Rental;

namespace RentWheels.Core.Contracts
{
    public interface IRentalService
    {
        Task RentCarAsync(RentCarFormViewModel model, int carId, string renterId, DateTime s, DateTime e);

        Task EndRentAsync(int rentalId);

        Task<IEnumerable<MyRentedCarsViewModel>> MyRentedCarsAsync(string renterId);

        Task<bool> IsCarValidForRentAsync(int carId, string renterId);

        Task<bool> RentalExistsAsync(int rentalId);

        Task<bool> HasRenterWithIdAsync(int rentalId, string renterId);

		Task EditAsync(int id, RentCarFormViewModel model, DateTime s, DateTime e);

		Task<RentCarFormViewModel> CreateRentalFormViewModelByIdAsync(int id);

        Task<IEnumerable<AdminAllRentalsViewModel>> AllRentals();
	}
}
