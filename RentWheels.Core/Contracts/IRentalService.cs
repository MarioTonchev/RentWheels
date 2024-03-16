using RentWheels.Core.VeiwModels.Rental;

namespace RentWheels.Core.Contracts
{
    public interface IRentalService
    {
        Task RentCarAsync(RentCarFormViewModel model, int carId, string renterId, DateTime s, DateTime e);
        Task EndRentAsync(int carId, string renterId);
        Task<IEnumerable<MyRentedCarsViewModel>> MyRentedCarsAsync(string renterId);
        Task<bool> IsCarValidForRentAsync(int carId);
        Task<bool> IsCarRentedBySameUserAsync(int carId, string renterId);
    }
}
