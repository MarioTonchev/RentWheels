using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using RentWheels.Core.Contracts;
using RentWheels.Core.Enumerations;
using RentWheels.Core.Services;
using RentWheels.Core.ViewModels.Car;
using RentWheels.Infrastructure.Common;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Tests
{
    [TestFixture]
    public class CarServiceTests
    {
        private readonly Mock<IRepository> repository;
        private readonly Mock<IEngineService> engineService;
        private readonly Mock<ICategoryService> categoryService;
        private readonly ICarService carService;
        private readonly List<Car> cars;

        public CarServiceTests()
        {
            repository = new Mock<IRepository>();
            engineService = new Mock<IEngineService>();
            categoryService = new Mock<ICategoryService>();

            carService = new CarService(repository.Object, engineService.Object, categoryService.Object);

            cars = new()
            {
                new Car()
                {
                    Id = 1,
                    Brand = "Audi",
                    Model = "A4",
                    Year = 2017,
                    Available = "true",
                    Color = "black metallic",
                    EngineId = 2,
                    PricePerDay = 100,
                    OwnerId = "572f859b-0afa-4112-aa5b-23a6d9560fca",
                    ImageUrl = "https://as1.ftcdn.net/v2/jpg/03/63/44/86/1000_F_363448659_uZxsIp3cObzOiDx6oDi20fb3QFoYVAJF.jpg",
                    CategoryId = 1,
                    Engine = new Engine()
                    {
                        Id = 2,
                        Name = "Medium",
                        Horsepower = 180,
                        Cubage = 2000,
                        FuelType = "diesel"
                    },
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Sedan",
                        DoorCount = 4
                    },
                    Owner = new IdentityUser()
                    {
                        Id = "572f859b-0afa-4112-aa5b-23a6d9560fca",
                        Email = "ivan@ivanov.bg",
                    }
                },

                new Car()
                {
                    Id = 2,
                    Brand = "BMW",
                    Model = "M5",
                    Year = 2018,
                    Available = "true",
                    Color = "royal blue",
                    EngineId = 4,
                    PricePerDay = 350,
                    OwnerId = "572f859b-0afa-4112-aa5b-23a6d9560fca",
                    ImageUrl = "https://as1.ftcdn.net/v2/jpg/04/35/92/40/1000_F_435924070_A2n5ZyQUF7nCRsYZj6SX1SAYOn5sggFh.jpg",
                    CategoryId = 1,
                    Engine = new Engine()
                    {
                        Id = 4,
                        Name = "Sport",
                        Horsepower = 500,
                        Cubage = 4000,
                        FuelType = "gasoline"
                    },
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Sedan",
                        DoorCount = 4
                    },
                    Owner = new IdentityUser()
                    {
                        Id = "572f859b-0afa-4112-aa5b-23a6d9560fca",
                        Email = "ivan@ivanov.bg",
                    }
                },

                new Car()
                {
                    Id = 3,
                    Brand = "Honda",
                    Model = "Civic",
                    Year = 2019,
                    Available = "true",
                    Color = "green",
                    EngineId = 3,
                    PricePerDay = 70,
                    OwnerId = "6410f766-0fef-4d08-9c02-44a9ffa5dfc5",
                    ImageUrl = "https://as1.ftcdn.net/v2/jpg/04/35/92/40/1000_F_435924070_A2n5ZyQUF7nCRsYZj6SX1SAYOn5sggFh.jpg",
                    CategoryId = 2,
                    Engine = new Engine()
                    {
                        Id = 3,
                        Name = "Big",
                        Horsepower = 240,
                        Cubage = 2200,
                        FuelType = "gasoline"
                    },
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Hatchback",
                        DoorCount = 4
                    },
                    Owner = new IdentityUser()
                    {
                        Id = "6410f766-0fef-4d08-9c02-44a9ffa5dfc5",
                        Email = "john@doe.bg"
                    }
                }
            };
        }

        [SetUp]
        public void SetUp()
        {
            repository.Setup(r => r.AllAsReadOnly<Car>()).Returns(cars.BuildMock().AsNoTracking());
            repository.Setup(r => r.All<Car>()).Returns(cars.BuildMock());
        }

        [Test]
        public async Task AllAsyncWithNoFilter()
        {
            var result = await carService.AllAsync();

            var firstCar = result.Cars.First();

            Assert.IsNotNull(result);
            Assert.That(firstCar.Id, Is.EqualTo(3));
            Assert.That(firstCar.Brand, Is.EqualTo("Honda"));
            Assert.That(firstCar.Year, Is.EqualTo(2019));
        }

        [Test]
        public async Task AllAsyncWithCategoryFilter()
        {
            var result = await carService.AllAsync(cateogry: "Sedan");

            Assert.IsNotNull(result);
            Assert.That(result.TotalCarsCount, Is.EqualTo(2));
            Assert.That(result.Cars.Any(c => c.Brand == "Honda"), Is.False);
        }

        [Test]
        public async Task AllAsyncWithSearchFilter()
        {
            var result = await carService.AllAsync(searchTerm: "A4");

            Assert.IsNotNull(result);
            Assert.That(result.TotalCarsCount, Is.EqualTo(1));
            Assert.That(result.Cars.Any(c => c.Brand == "Audi"));
        }

        [Test]
        public async Task AllAsyncWithSortByYearFilter()
        {
            var result = await carService.AllAsync(sorting: CarSorting.Year);

            var firstCar = result.Cars.First();

            Assert.IsNotNull(result);
            Assert.That(firstCar.Id, Is.EqualTo(3));
            Assert.That(firstCar.Year, Is.EqualTo(2019));
        }

        [Test]
        public async Task AllAsyncWithSortByPriceAscendingFilter()
        {
            var result = await carService.AllAsync(sorting: CarSorting.PriceAscending);

            var firstCar = result.Cars.First();

            Assert.IsNotNull(result);
            Assert.That(firstCar.Id, Is.EqualTo(3));
            Assert.That(firstCar.Brand, Is.EqualTo("Honda"));
        }

        [Test]
        public async Task AllAsyncWithSortByPriceDescendingFilter()
        {
            var result = await carService.AllAsync(sorting: CarSorting.PriceDescending);

            var firstCar = result.Cars.First();

            Assert.IsNotNull(result);
            Assert.That(firstCar.Id, Is.EqualTo(2));
            Assert.That(firstCar.Brand, Is.EqualTo("BMW"));
        }

        [Test]
        public async Task CarExistsAsyncShouldReturnTrue()
        {
            var result = await carService.CarExistsAsync(1);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CarExistsAsyncShouldReturnFalseWhenCarDoesNotExist()
        {
            var result = await carService.CarExistsAsync(55);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task CreateAsyncShouldReturnCreatedCarsId()
        {
            var carModel = new CarFormViewModel()
            {
                Brand = "Toyota",
                CarModel = "Corolla",
                Year = 2020,
                Color = "white",
                EngineId = 2,
                PricePerDay = 60,
                ImageUrl = "https://as1.ftcdn.net/v2/jpg/04/35/92/40/1000_F_435924070_A2n5ZyQUF7nCRsYZj6SX1SAYOn5sggFh.jpg",
                CategoryId = 1
            };

            Car? createdEntity = null;

            repository.Setup(r => r.AddAsync(It.IsAny<Car>()))
                          .Callback<Car>(entity => createdEntity = entity);


            await carService.CreateAsync(carModel, "572f859b-0afa-4112-aa5b-23a6d9560fca");

            Assert.IsNotNull(createdEntity);
            Assert.That(createdEntity.Brand, Is.EqualTo("Toyota"));
        }

        [Test]
        public async Task MyCarsAsyncShouldReturnCarsOfTheOwner()
        {
            var cars = await carService.MyCarsAsync("572f859b-0afa-4112-aa5b-23a6d9560fca");

            Assert.That(cars.Any(c => c.Brand == "BMW"));
        }

        [Test]
        public async Task DetailsAsyncShouldReturnCorrectModel()
        {
            CarDetailsViewModel model = await carService.DetailsAsync(3);

            Assert.IsNotNull(model);
            Assert.That(model.Id, Is.EqualTo(3));
            Assert.That(model.Brand, Is.EqualTo("Honda"));
        }

        [Test]
        public async Task EditAsyncShouldEditCorrectCar()
        {
            var carModel = new CarFormViewModel()
            {
                Brand = "Honda",
                CarModel = "Accord",
                Year = 2008,
                Color = "green",
                EngineId = 3,
                PricePerDay = 70,
                ImageUrl = "https://as1.ftcdn.net/v2/jpg/04/35/92/40/1000_F_435924070_A2n5ZyQUF7nCRsYZj6SX1SAYOn5sggFh.jpg",
                CategoryId = 1
            };

            repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await carService.EditAsync(3, carModel);

            var existingCar = cars.FirstOrDefault(c => c.Id == 3);

            Assert.That(carModel.Brand, Is.EqualTo(existingCar.Brand));
            Assert.That(carModel.CarModel, Is.EqualTo(existingCar.Model));
            Assert.That(carModel.Year, Is.EqualTo(existingCar.Year));
        }

        [Test]
        public async Task CreateCarFormViewModelByIdAsyncShouldReturnCorrectModel()
        {
            var carModel = await carService.CreateCarFormViewModelByIdAsync(1);

            var car = cars.FirstOrDefault(c => c.Id == 1);

            Assert.IsNotNull(carModel);
            Assert.That(carModel.Brand, Is.EqualTo(car.Brand));
            Assert.That(carModel.CarModel, Is.EqualTo(car.Model));
            Assert.That(carModel.Year, Is.EqualTo(car.Year));
            Assert.That(carModel.Color, Is.EqualTo(car.Color));
        }

        [Test]
        public async Task HasOwnerWithIdAsyncShouldReturnTrue()
        {
            var result = await carService.HasOwnerWithIdAsync(1, "572f859b-0afa-4112-aa5b-23a6d9560fca");

            Assert.True(result);
        }

        [Test]
        public async Task HasOwnerWithIdAsyncShouldReturnFalseIfItDoesNotHaveGivenOwnerId()
        {
            var result = await carService.HasOwnerWithIdAsync(1, "6410f766-0fef-4d08-9c02-44a9ffa5dfc5");

            Assert.IsFalse(result);
        }

        [Test]
        public async Task RemoveCarAsyncShouldDeleteCorrectCarById()
        {
            repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);
            repository.Setup(r => r.Delete(It.IsAny<Car>())).Callback((Car entity) =>
            {
                cars.Remove(entity);
            });

            await carService.RemoveCarAsync(1);

            Car? deletedCar = cars.FirstOrDefault(c => c.Id == 1);

            Assert.IsNull(deletedCar);
            Assert.IsNull(deletedCar?.Id);
        }

        [Test]
        public async Task AllCarsAsyncShouldReturnCorrectModel()
        {
            var model = await carService.AllCarsAsync();

            var firstCar = model.FirstOrDefault(c => c.Id == 1);

            Assert.IsNotNull(firstCar);
            Assert.That(firstCar.Id, Is.EqualTo(1));
            Assert.That(firstCar.Brand, Is.EqualTo("Audi"));
            Assert.That(firstCar.UserEmail, Is.EqualTo("ivan@ivanov.bg"));
        }
    }
}
