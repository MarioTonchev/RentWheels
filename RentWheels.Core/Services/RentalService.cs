using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RentWheels.Core.Contracts;
using RentWheels.Core.VeiwModels.Car;
using RentWheels.Core.VeiwModels.Rental;
using RentWheels.Infrastructure.Common;
using RentWheels.Infrastructure.Models;
using System.Globalization;
using static RentWheels.Infrastructure.Constants.DataConstants;

namespace RentWheels.Core.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRepository repository;

        public RentalService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task RentCarAsync(RentCarFormViewModel model, int carId, string renterId, DateTime s, DateTime e)
        {          
            var car = await repository.AllAsReadOnly<Car>().Where(c => c.Id == carId).FirstOrDefaultAsync();

            var rental = new Rental()
            {
                CarId = carId,
                RenterId = renterId,
                Start = s,
                End = e,
                PickUpLocation = model.PickUp,
                DropOffLocation = model.DropOff,
                TotalPrice = CalcualtePrice(s, e, car.PricePerDay)
            };

            await repository.AddAsync(rental);
            await repository.SaveChangesAsync();
        }
        public async Task<IEnumerable<MyRentedCarsViewModel>> MyRentedCarsAsync(string renterId)
        {
            return await repository.AllAsReadOnly<Rental>().Where(r => r.RenterId == renterId).Select(r => new MyRentedCarsViewModel()
            {
                Brand = r.Car.Brand,
                Model = r.Car.Model,
                Year = r.Car.Year
            }).ToListAsync();
        }

        public Task EndRentAsync()
        {
            throw new NotImplementedException();
        }

        private decimal CalcualtePrice(DateTime s, DateTime e, decimal pricePerDay)
        {
            int days = (e - s).Days;

            return pricePerDay * days;
        }
    }
}
