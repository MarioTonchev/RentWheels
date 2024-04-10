using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using RentWheels.Core.Contracts;
using RentWheels.Core.Services;
using RentWheels.Infrastructure.Common;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private readonly Mock<IRepository> repository;
        private readonly ICategoryService categoryService;
        private readonly List<Category> categories;

        public CategoryServiceTests()
        {
            repository = new Mock<IRepository>();

            categoryService = new CategoryService(repository.Object);

            categories = new()
            {
                new Category
                {
                    Id = 1,
                    Name = "Sedan",
                    DoorCount = 4
                },
                new Category
                {
                    Id = 2,
                    Name = "Hatchback",
                    DoorCount = 4
                },
                new Category
                {
                    Id = 3,
                    Name = "SUV",
                    DoorCount = 4
                },
                new Category
                {
                    Id = 4,
                    Name = "Estate",
                    DoorCount = 4
                },
                new Category
                {
                    Id = 5,
                    Name = "Coupe",
                    DoorCount = 2
                },
                new Category
                {
                    Id = 6,
                    Name = "Supercar",
                    DoorCount = 2
                }
            };
        }

        [SetUp]
        public void SetUp()
        {
            repository.Setup(r => r.AllAsReadOnly<Category>()).Returns(categories.BuildMock().AsNoTracking());
            repository.Setup(r => r.All<Category>()).Returns(categories.BuildMock());
        }

        [Test]
        public async Task CategoryExistsAsyncShouldReturnTrue()
        {
            var result = await categoryService.CategoryExistsAsync(2);

            Assert.True(result);
        }

        [Test]
        public async Task CategoryExistsAsyncShouldReturnFalseIfCategoryDoesNotExist()
        {
            var result = await categoryService.CategoryExistsAsync(10);

            Assert.False(result);
        }

        [Test]
        public async Task AllCategoriesNamesAsyncShouldReturnCorrectNames()
        {
            var result = await categoryService.AllCategoriesNamesAsync();

            Assert.That(result.Contains("Sedan"));
            Assert.That(result.Contains("Hatchback"));
        }

        [Test]
        public async Task AllCategoriesFormAsyncShouldReturnCorrectFormModels()
        {
            var result = await categoryService.AllCategoriesFormAsync();

            Assert.That(result.Any(c => c.Id == 1));
            Assert.That(result.Any(c => c.Id == 6));
            Assert.That(result.Any(c => c.Name == "Sedan"));
            Assert.That(result.Any(c => c.Name == "SUV"));
        }
    }
}
