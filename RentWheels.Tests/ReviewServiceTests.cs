using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using RentWheels.Core.Contracts;
using RentWheels.Core.Services;
using RentWheels.Core.ViewModels.Car;
using RentWheels.Core.ViewModels.Review;
using RentWheels.Infrastructure.Common;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Tests
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private readonly Mock<IRepository> repository;
        private readonly IReviewService reviewService;
        private readonly List<Review> reviews;

        public ReviewServiceTests()
        {
            repository = new Mock<IRepository>();

            reviewService = new ReviewService(repository.Object);

            reviews = new()
            {
                new Review
                {
                    Id = 1,
                    Rating = 6,
                    Comment = "Very good car! I am very impressed!",
                    CarId = 1,
                    ReviewerId = "6410f766-0fef-4d08-9c02-44a9ffa5dfc5",
                    Car = new Car()
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
                    Reviewer = new IdentityUser()
                    {
                        Id = "6410f766-0fef-4d08-9c02-44a9ffa5dfc5",
                        Email = "john@doe.bg"
                    }
                },
                new Review
                {
                    Id = 2,
                    Rating = 8,
                    Comment = "It drives pretty smooth. Worth the price!",
                    CarId = 2,
                    ReviewerId = "572f859b-0afa-4112-aa5b-23a6d9560fca",
                    Car = new Car()
                    {
                        Id = 2,
                        Brand = "Honda",
                        Model = "Civic",
                        Year = 2019,
                        Available = "true",
                        Color = "green",
                        EngineId = 3,
                        PricePerDay = 70,
                        OwnerId = "6410f766-0fef-4d08-9c02-44a9ffa5dfc5",
                        ImageUrl = "https://as1.ftcdn.net/v2/jpg/04/35/92/40/1000_F_435924070_A2n5ZyQUF7nCRsYZj6SX1SAYOn5sggFh.jpg",
                        CategoryId = 1,
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
                            Id = 1,
                            Name = "Sedan",
                            DoorCount = 4
                        },
                        Owner = new IdentityUser()
                        {
                            Id = "6410f766-0fef-4d08-9c02-44a9ffa5dfc5",
                            Email = "john@doe.bg"
                        }
                    },
                    Reviewer = new IdentityUser()
                    {
                        Id = "572f859b-0afa-4112-aa5b-23a6d9560fca",
                        Email = "ivan@ivanov.bg",
                    }
                },
                new Review
                {
                    Id = 3,
                    Rating = 7,
                    Comment = "Wow! I am impressed by this car.",
                    CarId = 3,
                    ReviewerId = "7a7e1653-d57f-4f41-97cb-e4bd92592fdc",
                    Car = new Car()
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
                        CategoryId = 1,
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
                            Id = 1,
                            Name = "Sedan",
                            DoorCount = 4
                        },
                        Owner = new IdentityUser()
                        {
                            Id = "6410f766-0fef-4d08-9c02-44a9ffa5dfc5",
                            Email = "john@doe.bg"
                        }
                    },
                    Reviewer = new IdentityUser()
                    {
                        Id = "7a7e1653-d57f-4f41-97cb-e4bd92592fdc",
                        Email = "pesho@peshov.bg",
                    }
                }
            };
        }

        [SetUp]
        public void SetUp()
        {
            repository.Setup(r => r.AllAsReadOnly<Review>()).Returns(reviews.BuildMock().AsNoTracking());
            repository.Setup(r => r.All<Review>()).Returns(reviews.BuildMock());
        }

        [Test]
        public async Task GetForCarAsyncShouldReturnCorrectModel()
        {
            var result = await reviewService.GetForCarAsync(1);

            Assert.IsNotNull(result);
            Assert.That(result.Any(r => r.Id == 1));
            Assert.That(result.Any(r => r.Rating == 6));
            Assert.That(result.Any(r => r.Comment == "Very good car! I am very impressed!"));
            Assert.That(result.Any(r => r.CarId == 1));
        }

        [Test]
        public async Task AddAsyncShouldAddCorrectReview()
        {
            var reviewModel = new ReviewFormViewModel()
            {
                CarId = 3,
                Comment = "Wow this car is something else!",
                Rating = 10
            };

            Review? createdEntity = null;

            repository.Setup(r => r.AddAsync(It.IsAny<Review>()))
                          .Callback<Review>(entity => createdEntity = entity);


            await reviewService.AddAsync(reviewModel, "572f859b-0afa-4112-aa5b-23a6d9560fca");

            Assert.IsNotNull(createdEntity);
            Assert.That(createdEntity.Comment, Is.EqualTo("Wow this car is something else!"));
            Assert.That(createdEntity.Rating, Is.EqualTo(10));
        }

        [Test]
        public async Task ReviewExistsAsyncShouldReturnTrue()
        {
            var result = await reviewService.ReviewExistsAsync(2);

            Assert.True(result);
        }

        [Test]
        public async Task ReviewExistsAsyncShouldReturnFalseIfReviewDoesNotExist()
        {
            var result = await reviewService.ReviewExistsAsync(4);

            Assert.False(result);
        }

        [Test]
        public async Task HasReviewerWithIdAsyncShouldReturnTrue()
        {
            var result = await reviewService.HasReviewerWithIdAsync(1, "6410f766-0fef-4d08-9c02-44a9ffa5dfc5");

            Assert.True(result);
        }

        [Test]
        public async Task HasReviewerWithIdAsyncShouldReturnFalseIfDoesNotHaveGivenReviewerId()
        {
            var result = await reviewService.HasReviewerWithIdAsync(2, "7a7e1653-d57f-4f41-97cb-e4bd92592fdc");

            Assert.False(result);
        }

        [Test]
        public async Task RemoveReviewAsyncShouldRemoveCorrectReview()
        {
            repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);
            repository.Setup(r => r.Delete(It.IsAny<Review>())).Callback((Review entity) =>
            {
                reviews.Remove(entity);
            });

            await reviewService.RemoveReviewAsync(1);

            Review? deletedReview = reviews.FirstOrDefault(c => c.Id == 1);

            Assert.IsNull(deletedReview);
            Assert.IsNull(deletedReview?.Id);
        }

        [Test]
        public async Task GetReviewCarIdAsyncShouldReturnCorrectCarId()
        {
            var result = await reviewService.GetReviewCarIdAsync(2);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public async Task CreateReviewFormViewModelByIdAsyncShouldReturnCorrectModel()
        {
            var reviewModel = await reviewService.CreateReviewFormViewModelByIdAsync(3);

            var review = reviews.FirstOrDefault(r => r.Id == 3);

            Assert.IsNotNull(reviewModel);
            Assert.That(reviewModel.Rating, Is.EqualTo(review.Rating));
            Assert.That(reviewModel.Comment, Is.EqualTo(review.Comment));
            Assert.That(reviewModel.CarId, Is.EqualTo(review.CarId));
        }

        [Test]
        public async Task EditAsyncShouldEditCorrectReview()
        {
            var reviewModel = new ReviewFormViewModel()
            {
                Rating = 10,
                Comment = "An amazing car which is very fun to drive!",
                CarId = 3
            };

            repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await reviewService.EditAsync(3, reviewModel);

            var existingReview = reviews.FirstOrDefault(r => r.Id == 3);

            Assert.That(reviewModel.Rating, Is.EqualTo(existingReview.Rating));
            Assert.That(reviewModel.Comment, Is.EqualTo(existingReview.Comment));
            Assert.That(reviewModel.CarId, Is.EqualTo(existingReview.CarId));
        }
    }
}
