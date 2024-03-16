using RentWheels.Core.VeiwModels.Car;
using RentWheels.Core.VeiwModels.Rental;

namespace RentWheels.Core.Contracts
{
    public interface IRentalService
    {
        Task RentCarAsync(RentCarFormViewModel model, int carId, string renterId, DateTime s, DateTime e);
        Task EndRentAsync();
        Task<IEnumerable<MyRentedCarsViewModel>> MyRentedCarsAsync(string renterId);
    }
}
