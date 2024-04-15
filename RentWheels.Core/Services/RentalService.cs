using Microsoft.EntityFrameworkCore;
using RentWheels.Core.Contracts;
using RentWheels.Core.ViewModels.Admin;
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
                TotalPrice = CalcualteTotalPrice(s, e, car.PricePerDay),
                IsActive = "true"
            };

            car.Available = "false";

            await repository.AddAsync(rental);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyRentedCarsViewModel>> MyRentedCarsAsync(string renterId)
        {
            return await repository.AllAsReadOnly<Rental>().Where(r => r.RenterId == renterId && r.IsActive == "true")
                .Select(r => new MyRentedCarsViewModel()
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

        public async Task EndRentAsync(int rentalId)
        {
            var rental = await repository.All<Rental>().Where(r => r.Id == rentalId).Include(c => c.Car)
                .FirstOrDefaultAsync();

            if (rental != null) 
            {
                rental.Car.Available = "true";
                rental.IsActive = "false";
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

            if (car.Rentals.Any(r => r.CarId == carId && r.IsActive == "true"))
            {
                return false;
            }

            return true;
        }

		public async Task<bool> RentalExistsAsync(int rentalId)
		{
			if (await repository.AllAsReadOnly<Rental>().AnyAsync(r => r.Id == rentalId && r.IsActive == "true"))
			{
				return true;
			}

			return false;
		}
		
		public async Task<bool> HasRenterWithIdAsync(int rentalId, string renterId)
		{
            return await repository.AllAsReadOnly<Rental>().AnyAsync(r => r.Id == rentalId && r.RenterId == renterId && r.IsActive == "true");
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

        public async Task<IEnumerable<AdminAllRentalsViewModel>> AllActiveRentalsAsync()
        {
            var rentals = await repository.AllAsReadOnly<Rental>().Where(r => r.IsActive == "true")
                .Select(r => new AdminAllRentalsViewModel()
            {
                RentalId = r.Id,
                CarId = r.CarId,
                Brand = r.Car.Brand,
                CarModel = r.Car.Model,
                PickUp = r.PickUpLocation,
                DropOff = r.DropOffLocation,
                Start = r.Start.ToString(DateFormated),
                End = r.End.ToString(DateFormated),
                TotalPrice = r.TotalPrice,
                UserEmail = r.Renter.Email
            }).ToListAsync();

            return rentals;
        }

        public async Task<IEnumerable<RentHistoryViewModel>> MyRentHistoryAsync(string renterId)
        {
            return await repository.AllAsReadOnly<Rental>().Where(r => r.RenterId == renterId)
                .OrderByDescending(r => r.IsActive).ThenBy(r => r.Id)
                .Select(r => new RentHistoryViewModel()
                {
                    RentalId = r.Id,
                    CarId = r.CarId,
                    Brand = r.Car.Brand,
                    CarModel = r.Car.Model,
                    Start = r.Start.ToString(DateFormated),
                    End = r.End.ToString(DateFormated),
                    TotalPrice = r.TotalPrice,
                    PickUp = r.PickUpLocation,
                    DropOff = r.DropOffLocation,
                    IsActive = r.IsActive
                }).ToListAsync();
        }

		public async Task<IEnumerable<RentalMessageViewModel>> MyRentedCarsForSendingMessageAsync(string renterId)
		{
			return await repository.AllAsReadOnly<Rental>().Where(r => r.RenterId == renterId && r.IsActive == "true")
				.Select(r => new RentalMessageViewModel()
				{
					Id = r.Id,
                    CarBrand = r.Car.Brand,
                    CarModel = r.Car.Model
				}).ToListAsync();
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
