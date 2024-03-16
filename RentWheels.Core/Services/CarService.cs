using Microsoft.EntityFrameworkCore;
using RentWheels.Core.Contracts;
using RentWheels.Core.VeiwModels.Car;
using RentWheels.Core.VeiwModels.Category;
using RentWheels.Core.VeiwModels.Engine;
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
                Brand = c.Brand,
                Model = c.Model,
                Year = c.Year,
                ImageUrl = c.ImageUrl
            }).ToListAsync();
        }

        public async Task<IEnumerable<CarAllViewModel>> LastThreeCarsAsync()
        {
            return await repository.AllAsReadOnly<Car>().OrderByDescending(c => c.Id)
                .Select(c => new CarAllViewModel()
                {
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl
                }).Take(3).ToListAsync();
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

        public async Task<bool> EngineExistsAsync(int engineId)
        {
            return await repository.AllAsReadOnly<Engine>().AnyAsync(e => e.Id == engineId);
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
            throw new NotImplementedException();
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
    }
}