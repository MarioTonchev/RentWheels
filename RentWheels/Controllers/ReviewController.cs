using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;
using RentWheels.Core.ViewModels.Review;
using System.Security.Claims;

namespace RentWheels.Controllers
{
	public class ReviewController : BaseController
	{
		private readonly IReviewService reviewService;

        public ReviewController(IReviewService _reviewService)
        {
			reviewService = _reviewService;
        }

        [HttpGet]
		public async Task<IActionResult> AllByCar(int id)
		{
			var model = await reviewService.GetForCarAsync(id);

			ViewData["carId"] = id;

			return View(model);
		}

		[HttpGet]
		public IActionResult Add(int id)
		{
			var model = new ReviewFormViewModel()
			{
				CarId = id
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(ReviewFormViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await reviewService.AddAsync(model, User.Id());

			return RedirectToAction("AllByCar", new { id = model.CarId});
		}
	}
}
