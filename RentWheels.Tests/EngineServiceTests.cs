using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using RentWheels.Core.Contracts;
using RentWheels.Core.Services;
using RentWheels.Core.ViewModels.Car;
using RentWheels.Core.ViewModels.Engine;
using RentWheels.Infrastructure.Common;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Tests
{
    [TestFixture]
    public class EngineServiceTests
    {
        private readonly Mock<IRepository> repository;
        private readonly IEngineService engineService;
        private readonly List<Engine> engines;

        public EngineServiceTests()
        {
            repository = new Mock<IRepository>();

            engineService = new EngineService(repository.Object);

            engines = new()
            {
                new Engine()
                {
                    Id = 1,
                    Name = "Small",
                    Horsepower = 100,
                    Cubage = 1400,
                    FuelType = "diesel"
                },

                new Engine()
                {
                    Id = 2,
                    Name = "Medium",
                    Horsepower = 180,
                    Cubage = 2000,
                    FuelType = "diesel"
                },

                 new Engine()
                {
                    Id = 3,
                    Name = "Big",
                    Horsepower = 240,
                    Cubage = 2200,
                    FuelType = "gasoline"
                },

                new Engine()
                {
                    Id = 4,
                    Name = "Sport",
                    Horsepower = 500,
                    Cubage = 4000,
                    FuelType = "gasoline"
                }
            };
        }

        [SetUp]
        public void SetUp()
        {
            repository.Setup(r => r.AllAsReadOnly<Engine>()).Returns(engines.BuildMock().AsNoTracking());
            repository.Setup(r => r.All<Engine>()).Returns(engines.BuildMock());
        }

        [Test]
        public async Task DetailsAsyncShouldReturnCorrectModel()
        {
            EngineDetailsViewModel model = await engineService.DetailsAsync(4);

            Assert.IsNotNull(model);
            Assert.That(model.Name, Is.EqualTo("Sport"));
            Assert.That(model.Horsepower, Is.EqualTo(500));
            Assert.That(model.Cubage, Is.EqualTo(4000));
            Assert.That(model.FuelType, Is.EqualTo("gasoline"));
        }

        [Test]
        public async Task EngineExistsAsyncShouldReturnTrue()
        {
            var result = await engineService.EngineExistsAsync(1);

            Assert.True(result);
        }

        [Test]
        public async Task EngineExistsAsyncShouldReturnFalseIfEngineDoesNotExist()
        {
            var result = await engineService.EngineExistsAsync(7);

            Assert.False(result);
        }

        [Test]
        public async Task AllEnginesAsyncShouldReturnCorrectModel()
        {
            var result = await engineService.AllEnginesAsync();
            var engineModel = result.FirstOrDefault(e => e.Name == "Medium");

            var existingEngine = engines.FirstOrDefault(e => e.Name == "Medium");

            Assert.IsNotNull(result);
            Assert.That(existingEngine.Name, Is.EqualTo(engineModel.Name));
            Assert.That(existingEngine.Horsepower, Is.EqualTo(engineModel.Horsepower));
            Assert.That(existingEngine.Cubage, Is.EqualTo(engineModel.Cubage));
            Assert.That(existingEngine.FuelType, Is.EqualTo(engineModel.FuelType));
        }

        [Test]
        public async Task AllEnginesFormAsyncShouldReturnCorrectFormModels()
        {
            var result = await engineService.AllEnginesFormAsync();

            Assert.That(result.Any(e => e.Id == 1));
            Assert.That(result.Any(e => e.Id == 4));
            Assert.That(result.Any(e => e.Name == "Small"));
            Assert.That(result.Any(e => e.Name == "Medium"));
            Assert.That(result.Any(e => e.Name == "Big"));
            Assert.That(result.Any(e => e.Name == "Sport"));
        }
    }
}
