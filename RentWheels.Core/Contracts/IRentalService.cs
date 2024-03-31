using RentWheels.Core.ViewModels.Rental;

namespace RentWheels.Core.Contracts
{
    public interface IRentalService
    {
        Task RentCarAsync(RentCarFormViewModel model, int carId, string renterId, DateTime s, DateTime e);

        Task EndRentAsync(int carId, string renterId);

        Task<IEnumerable<MyRentedCarsViewModel>> MyRentedCarsAsync(string renterId);

        Task<bool> IsCarValidForRentAsync(int carId, string renterId);

        Task<bool> RentalExistsAsync(int carId, string renterId);

        Task<IEnumerable<MyLendedCarsViewModel>> MyLendedCarsAsync(string ownerId);
    }
}
