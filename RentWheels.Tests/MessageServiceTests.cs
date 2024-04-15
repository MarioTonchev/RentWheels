using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using RentWheels.Core.Contracts;
using RentWheels.Core.Services;
using RentWheels.Core.ViewModels.Message;
using RentWheels.Core.ViewModels.Rental;
using RentWheels.Infrastructure.Common;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Tests
{
	[TestFixture]
	public class MessageServiceTests
	{
		private readonly Mock<IRepository> repository;
		private readonly IMessageService messageService;
		private List<Message> messages;
		private List<Rental> rentals;

		public MessageServiceTests()
		{
			repository = new Mock<IRepository>();
			messageService = new MessageService(repository.Object);

			messages = new()
			{
				new Message()
				{
					Id = 1,
					Title = "TestMessage",
					Content = "This is the first test message. Everything written here is fake",
					CreatedOn = DateTime.Now,
					SenderId = "572f859b-0afa-4112-aa5b-23a6d9560fca",
					ReceiverId = "6410f766-0fef-4d08-9c02-44a9ffa5dfc5",
					Sender = new IdentityUser()
					{
						Id = "572f859b-0afa-4112-aa5b-23a6d9560fca",
						UserName = "ivan@ivanov.bg"
					},
					Receiver = new IdentityUser()
					{
						Id = "6410f766-0fef-4d08-9c02-44a9ffa5dfc5",
						UserName = "john@doe.bg"
					}
				},

				new Message()
				{
					Id = 2,
					Title = "TestMessage2",
					Content = "This is the second test message. Everything written here is fake",
					CreatedOn = DateTime.Now,
					SenderId = "572f859b-0afa-4112-aa5b-23a6d9560fca",
					ReceiverId = "7a7e1653-d57f-4f41-97cb-e4bd92592fdc",
					Sender = new IdentityUser()
					{
						Id = "572f859b-0afa-4112-aa5b-23a6d9560fca",
						UserName = "ivan@ivanov.bg"
					},
					Receiver = new IdentityUser()
					{
						Id = "7a7e1653-d57f-4f41-97cb-e4bd92592fdc",
						UserName = "pesho@peshov.bg"
					}
				},

				new Message()
				{
					Id = 3,
					Title = "TestMessage3",
					Content = "This is the third test message. Everything written here is fake",
					CreatedOn = DateTime.Now,
					SenderId = "448ca120-e9e3-4358-ac74-7ce71c453d63",
					ReceiverId = "6410f766-0fef-4d08-9c02-44a9ffa5dfc5",
					Sender = new IdentityUser()
					{
						Id = "448ca120-e9e3-4358-ac74-7ce71c453d63",
						UserName = "admin@shef.com"
					},
					Receiver = new IdentityUser()
					{
						Id = "6410f766-0fef-4d08-9c02-44a9ffa5dfc5",
						UserName = "john@doe.bg"
					}
				}
			};

			rentals = new()
			{
				new Rental()
				{
					Id = 1,
					PickUpLocation = "PickUpTest1",
					DropOffLocation = "DropOffTest1",
					IsActive = "true",
					CarId = 1,
					Car = new Car()
					{
						Id = 1,
						Brand = "Audi",
						Model = "A4",
						Year = 2017,
						Available = "false",
						Color = "black metallic",
						EngineId = 2,
						PricePerDay = 100,
						OwnerId = "572f859b-0afa-4112-aa5b-23a6d9560fca",
						ImageUrl = "https://as1.ftcdn.net/v2/jpg/03/63/44/86/1000_F_363448659_uZxsIp3cObzOiDx6oDi20fb3QFoYVAJF.jpg",
						CategoryId = 1,
						Owner = new IdentityUser()
						{
							Id = "572f859b-0afa-4112-aa5b-23a6d9560fca",
							Email = "ivan@ivanov.bg",
						}
					}
				},

				new Rental()
				{
					Id = 2,
					PickUpLocation = "PickUpTest2",
					DropOffLocation = "DropOffTest2",
					IsActive = "true",
					CarId = 2,
					Car = new Car()
					{
						Id = 2,
						Brand = "BMW",
						Model = "5-Series",
						Year = 2018,
						Available = "false",
						Color = "blue",
						EngineId = 2,
						PricePerDay = 90,
						OwnerId = "6410f766-0fef-4d08-9c02-44a9ffa5dfc5",
						ImageUrl = "https://as1.ftcdn.net/v2/jpg/03/63/44/86/1000_F_363448659_uZxsIp3cObzOiDx6oDi20fb3QFoYVAJF.jpg",
						CategoryId = 1,
						Owner = new IdentityUser()
						{
							Id = "6410f766-0fef-4d08-9c02-44a9ffa5dfc5",
							UserName = "john@doe.bg"
						}
					}
				},				
			};
		}

		[SetUp]
		public void SetUp()
		{
			repository.Setup(r => r.AllAsReadOnly<Message>()).Returns(messages.BuildMock().AsNoTracking());
			repository.Setup(r => r.All<Message>()).Returns(messages.BuildMock());
			repository.Setup(r => r.AllAsReadOnly<Rental>()).Returns(rentals.BuildMock().AsNoTracking());
		}

		[Test]
		public async Task MyMessagesShouldReturnCorrectModel()
		{
			var result = await messageService.MyMessagesAsync("7a7e1653-d57f-4f41-97cb-e4bd92592fdc");

			var message = result.FirstOrDefault();

			Assert.IsNotNull(result);
			Assert.That(result.Count(), Is.EqualTo(1));
			Assert.That(message.Title, Is.EqualTo("TestMessage2"));
			Assert.That(message.Content, Is.EqualTo("This is the second test message. Everything written here is fake"));
		}

		[Test]
		public async Task SendAsyncShouldAddMessageToDatabase()
		{
			var messageModel = new MessageFormViewModel()
			{
				Title = "TestMessage4",
				Content = "This is the fourth test message. Everything written here is fake",
				RentalId = 1,
				Rentals = new List<RentalMessageViewModel>()
				{
					new RentalMessageViewModel()
					{
						Id = 1,
						CarBrand = "Honda",
						CarModel = "Accord"
					},
					new RentalMessageViewModel()
					{
						Id = 2,
						CarBrand = "Audi",
						CarModel = "A4"
					},
					new RentalMessageViewModel()
					{
						Id = 3,
						CarBrand = "BMW",
						CarModel = "5-Series"
					}
				}
			};

			Message? createdEntity = null;

			repository.Setup(r => r.AddAsync(It.IsAny<Message>()))
						  .Callback<Message>(entity => createdEntity = entity);

			await messageService.SendAsync(messageModel, "448ca120-e9e3-4358-ac74-7ce71c453d63");

			Assert.IsNotNull(createdEntity);
			Assert.That(createdEntity.Title, Is.EqualTo("TestMessage4"));
			Assert.That(createdEntity.Content, Is.EqualTo("This is the fourth test message. Everything written here is fake"));
			Assert.That(createdEntity.SenderId, Is.EqualTo("448ca120-e9e3-4358-ac74-7ce71c453d63"));
			Assert.That(createdEntity.SenderId, Is.EqualTo("448ca120-e9e3-4358-ac74-7ce71c453d63"));
			Assert.That(createdEntity.ReceiverId, Is.EqualTo("572f859b-0afa-4112-aa5b-23a6d9560fca"));
		}
	}
}
