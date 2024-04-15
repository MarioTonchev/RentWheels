using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;
using RentWheels.Core.ViewModels.Message;
using System.Security.Claims;

namespace RentWheels.Controllers
{
	public class MessageController : BaseController
	{
		private readonly IMessageService messageService;
		private readonly IRentalService rentalService;

        public MessageController(
			IMessageService _messageService,
			IRentalService _rentalService)
        {
            messageService = _messageService;
			rentalService = _rentalService;
        }

		[HttpGet]
		public async Task<IActionResult> MyMessages()
		{
			var model = await messageService.MyMessagesAsync(User.Id());

			return View(model);
		}

        [HttpGet]
		public async Task<IActionResult> SendMessage()
		{
			var model = new MessageFormViewModel();

			model.Rentals = await rentalService.MyRentedCarsForSendingMessageAsync(User.Id());

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> SendMessage(MessageFormViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Rentals = await rentalService.MyRentedCarsForSendingMessageAsync(User.Id());
				return View(model);
			}

			await messageService.SendAsync(model, User.Id());

			return RedirectToAction("All", "Car");
		}
	}
}
