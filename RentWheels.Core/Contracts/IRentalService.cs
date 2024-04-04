using RentWheels.Core.ViewModels.Rental;

namespace RentWheels.Core.Contracts
{
    public interface IRentalService
    {
        Task RentCarAsync(RentCarFormViewModel model, int carId, string renterId, DateTime s, DateTime e);

        Task EndRentAsync(int rentalId, string renterId);

        Task<IEnumerable<MyRentedCarsViewModel>> MyRentedCarsAsync(string renterId);

        Task<bool> IsCarValidForRentAsync(int carId, string renterId);

        Task<bool> RentalExistsAsync(int rentalId, string renterId);

        Task<bool> RentalExistsAsync(int rentalId);

		Task<IEnumerable<MyLendedCarsViewModel>> MyLendedCarsAsync(string ownerId);

        Task<bool> HasRenterWithIdAsync(int rentalId, string renterId);

		Task EditAsync(int id, RentCarFormViewModel model, DateTime s, DateTime e);

		Task<RentCarFormViewModel> CreateRentalFormViewModelByIdAsync(int id);
	}
}
