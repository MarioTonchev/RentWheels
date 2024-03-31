﻿using Microsoft.EntityFrameworkCore;
using RentWheels.Core.Contracts;
using RentWheels.Core.Enumerations;
using RentWheels.Core.ViewModels.Car;
using RentWheels.Core.ViewModels.Category;
using RentWheels.Core.ViewModels.Engine;
using RentWheels.Infrastructure.Common;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Core.Services
{
    public class CarService : ICarService
    {
        private readonly IRepository repository;

        public CarService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<CarAllViewModel>> AllCarsAsync()
        {
            return await repository.AllAsReadOnly<Car>().Select(c => new CarAllViewModel()
            {
                Id = c.Id,
                Brand = c.Brand,
                CarModel = c.Model,
                Year = c.Year,
                ImageUrl = c.ImageUrl,
                OwnerId = c.OwnerId
            }).ToListAsync();
        }

        public async Task<IEnumerable<EngineAllViewModel>> AllEnginesAsync()
        {
            return await repository.AllAsReadOnly<Engine>().Select(e => new EngineAllViewModel()
            {
                Name = e.Name,
                Horsepower = e.Horsepower,
                Cubage = e.Cubage,
                FuelType = e.FuelType,
            }).ToListAsync();
        }

        public async Task<IEnumerable<EngineFormViewModel>> AllEnginesFormAsync()
        {
            return await repository.AllAsReadOnly<Engine>().Select(e => new EngineFormViewModel()
            {
                Id = e.Id,
                Name = e.Name
            }).ToListAsync();
        }

        public async Task<IEnumerable<CategoryViewModel>> AllCategoriesFormAsync()
        {
            return await repository.AllAsReadOnly<Category>().Select(e => new CategoryViewModel()
            {
                Id = e.Id,
                Name = e.Name
            }).ToListAsync();
        }

        public async Task<bool> CarExistsAsync(int carId)
        {
            return await repository.AllAsReadOnly<Car>().AnyAsync(c => c.Id == carId);
        }

        public async Task<int> CreateAsync(CarFormViewModel model, string ownerId)
        {
            var car = new Car()
            {
                Brand = model.Brand,
                Model = model.CarModel,
                Year = model.Year,
                ImageUrl = model.ImageUrl,
                Color = model.Color,
                EngineId = model.EngineId,
                PricePerDay = model.PricePerDay,
                Available = "true",
                OwnerId = ownerId,
                CategoryId = model.CategoryId
            };

            await repository.AddAsync(car);
            await repository.SaveChangesAsync();

            return car.Id;
        }

        public async Task<CarDetailsViewModel> DetailsAsync(int carId)
        {
            var car = await repository.AllAsReadOnly<Car>().Where(c => c.Id == carId).Include(c => c.Engine).Include(c => c.Category)
                .FirstOrDefaultAsync();

            var model = new CarDetailsViewModel()
            {
                Id = carId,
                Brand = car.Brand,
                CarModel = car.Model,
                Year = car.Year,
                ImageUrl = car.ImageUrl,
                Color = car.Color,
                EngineId = car.EngineId,
                PricePerDay = car.PricePerDay,
                Available = car.Available,
                CategoryName = car.Category.Name
            };

            return model;
        }

        public async Task EditAsync(int id, CarFormViewModel model)
        {
            var car = await repository.All<Car>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (car != null)
            {
                car.Brand = model.Brand;
                car.Model = model.CarModel;
                car.Year = model.Year;
                car.ImageUrl = model.ImageUrl;
                car.Color = model.Color;
                car.EngineId = model.EngineId;
                car.CategoryId = model.CategoryId;

                await repository.SaveChangesAsync();
            }
        }

        public async Task<CarFormViewModel> CreateCarFormViewModelByIdAsync(int id)
        {
            var car = await repository.AllAsReadOnly<Car>().Where(c => c.Id == id)
                .Select(c => new CarFormViewModel()
                {
                    Brand = c.Brand,
                    CarModel = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl,
                    Color = c.Color,
                    EngineId = c.EngineId,
                    PricePerDay = c.PricePerDay,
                    CategoryId = c.CategoryId
                }).FirstOrDefaultAsync();

            if (car != null)
            {
                car.Engines = await AllEnginesFormAsync();
                car.Categories = await AllCategoriesFormAsync();
            }

            return car;
        }

        public async Task<bool> HasOwnerWithIdAsync(int carId, string ownerId)
        {
            return await repository.AllAsReadOnly<Car>()
                .AnyAsync(c => c.Id == carId && c.OwnerId == ownerId);
        }

        public async Task RemoveCarAsync(int id)
        {
            var car = await repository.All<Car>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (car != null)
            {
                repository.Delete(car);
                await repository.SaveChangesAsync();
            }
        }

		public async Task<CarQueryViewModel> AllAsync(string? cateogry = null, string? searchTerm = null, CarSorting sorting = CarSorting.Newest, int currentPage = 1, int carPerPage = 4)
		{
            var cars = repository.AllAsReadOnly<Car>();

            if (cateogry != null)
            {
                cars = cars.Where(c => c.Category.Name == cateogry);
            }

            if (searchTerm != null)
            {
                string normalizedSearchTerm = searchTerm.ToLower();
                cars = cars.Where(c => c.Brand.ToLower().Contains(normalizedSearchTerm) || c.Model.ToLower().Contains(normalizedSearchTerm));
            }

            switch (sorting)
            {
                case CarSorting.Year:
                    cars = cars.OrderByDescending(c => c.Year);
                    break;
                case CarSorting.PriceAscending:
                    cars = cars.OrderBy(c => c.PricePerDay);
                    break;
                case CarSorting.PriceDescending:
                    cars = cars.OrderByDescending(c => c.PricePerDay);
                    break;
                default:
                    cars = cars.OrderByDescending(c => c.Id);
                    break;
            }

			var carsPaginated = await cars
				.Skip((currentPage - 1) * carPerPage)
				.Take(carPerPage)
				.Select(c => new CarAllViewModel()
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    CarModel = c.Model,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year,
                    OwnerId = c.OwnerId
                })
				.ToListAsync();

			int totalCars = await cars.CountAsync();
            int totalPages = (int)Math.Ceiling(totalCars / (double)carPerPage);

			return new CarQueryViewModel()
			{
				Cars = carsPaginated,
				TotalCarsCount = totalCars,
                TotalPages = totalPages
			};
		}

		public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
		{
            return await repository.AllAsReadOnly<Category>().Select(c => c.Name).ToListAsync();
		}
	}
}