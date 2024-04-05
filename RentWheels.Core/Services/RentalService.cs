using Microsoft.EntityFrameworkCore;
using RentWheels.Core.Contracts;
using RentWheels.Core.ViewModels.Rental;
using RentWheels.Infrastructure.Common;
using RentWheels.Infrastructure.Models;
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
            var car = await repository.All<Car>().Where(c => c.Id == carId).FirstOrDefaultAsync();

            var rental = new Rental()
            {
                CarId = carId,
                RenterId = renterId,
                Start = s,
                End = e,
                PickUpLocation = model.PickUp,
                DropOffLocation = model.DropOff,
                TotalPrice = CalcualteTotalPrice(s, e, car.PricePerDay)
            };

            car.Available = "false";

            await repository.AddAsync(rental);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyRentedCarsViewModel>> MyRentedCarsAsync(string renterId)
        {
            return await repository.AllAsReadOnly<Rental>().Where(r => r.RenterId == renterId).Select(r => new MyRentedCarsViewModel()
            {
                RentalId = r.Id,
                CarId = r.CarId,
                Brand = r.Car.Brand,
                CarModel = r.Car.Model,
                Start = r.Start.ToString(DateFormated),
                End = r.End.ToString(DateFormated),
                TotalPrice = r.TotalPrice,
                PickUp = r.PickUpLocation,
                DropOff = r.DropOffLocation
            }).ToListAsync();
        }

        public async Task EndRentAsync(int rentalId, string renterId)
        {
            var rental = await repository.All<Rental>().Where(r => r.Id == rentalId && r.RenterId == renterId).Include(c => c.Car)
                .FirstOrDefaultAsync();

            if (rental != null) 
            {
                rental.Car.Available = "true";
                repository.Delete(rental);
                await repository.SaveChangesAsync();
            }
        }

        public async Task<bool> IsCarValidForRentAsync(int carId, string renterId)
        {
            var car = await repository.AllAsReadOnly<Car>().Where(c => c.Id == carId).FirstOrDefaultAsync();

            if (car.OwnerId == renterId)
            {
                return false;
            }

            if (car.Available == "false")
            {
                return false;
            }

            if (car.Rentals.Any(r => r.CarId == carId))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> RentalExistsAsync(int rentalId, string renterId)
        {
            if (await repository.AllAsReadOnly<Rental>().AnyAsync(r => r.Id == rentalId && r.RenterId == renterId))
            {
                return true;
            }

            return false;
        }

		public async Task<bool> RentalExistsAsync(int rentalId)
		{
			if (await repository.AllAsReadOnly<Rental>().AnyAsync(r => r.Id == rentalId))
			{
				return true;
			}

			return false;
		}
		
		public async Task<bool> HasRenterWithIdAsync(int rentalId, string renterId)
		{
            return await repository.AllAsReadOnly<Rental>().AnyAsync(r => r.Id == rentalId && r.RenterId == renterId);
		}

		public async Task<RentCarFormViewModel> CreateRentalFormViewModelByIdAsync(int id)
		{
            return await repository.AllAsReadOnly<Rental>().Where(r => r.Id == id).Select(r => new RentCarFormViewModel()
            {
                Start = r.Start.ToString(DateFormated),
                End = r.End.ToString(DateFormated),
                PickUp = r.PickUpLocation,
                DropOff = r.DropOffLocation
            }).FirstOrDefaultAsync();
		}

		public async Task EditAsync(int id, RentCarFormViewModel model, DateTime s, DateTime e)
		{
            var rentalToEdit = await repository.All<Rental>().Where(r => r.Id == id).FirstOrDefaultAsync();

            if (rentalToEdit != null)
            {
                rentalToEdit.PickUpLocation = model.PickUp;
                rentalToEdit.DropOffLocation = model.DropOff;
                rentalToEdit.Start = s;
                rentalToEdit.End = e;

                rentalToEdit.TotalPrice = CalcualteTotalPrice(s, e, await FindCarPricePerDayAsync(rentalToEdit.CarId));

                await repository.SaveChangesAsync();
            }
		}

        private async Task<decimal> FindCarPricePerDayAsync(int carId)
        {
            var car = await repository.AllAsReadOnly<Car>().Where(c => c.Id == carId).FirstOrDefaultAsync();

            return car.PricePerDay;
        }

        private decimal CalcualteTotalPrice(DateTime s, DateTime e, decimal pricePerDay)
        {
            decimal result = pricePerDay;

            int days = Math.Abs((e - s).Days);

            if (days == 0)
            {
                return result;
            }

            result += pricePerDay * days;

            return result;
        }
	}
}
