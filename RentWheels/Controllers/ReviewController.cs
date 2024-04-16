using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;
using RentWheels.Core.ViewModels.Review;
using System.Security.Claims;

namespace RentWheels.Controllers
{
	public class ReviewController : BaseController
	{
		private readonly IReviewService reviewService;
		private readonly ICarService carService;

        public ReviewController(
			IReviewService _reviewService,
			ICarService _carService)
        {
			reviewService = _reviewService;
			carService = _carService;
		}

        [HttpGet]
		public async Task<IActionResult> AllByCar(int id)
		{
			if (await carService.CarExistsAsync(id) == false)
			{
				return BadRequest();
			}

			var model = await reviewService.GetForCarAsync(id);

			ViewData["carId"] = id;

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Add(int id)
		{
			if (await carService.HasOwnerWithIdAsync(id, User.Id()) == true)
			{
				return RedirectToAction(nameof(AllByCar), new { id = id});
			}

			var model = new ReviewFormViewModel()
			{
				CarId = id
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(ReviewFormViewModel model)
		{
			if (await carService.HasOwnerWithIdAsync(model.CarId, User.Id()) == true)
			{
				return RedirectToAction(nameof(AllByCar), new { id = model.CarId });
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await reviewService.AddAsync(model, User.Id());

            TempData["message"] = "Review successfully added.";
            TempData["messageType"] = "success";

            return RedirectToAction(nameof(AllByCar), new { id = model.CarId});
		}

		[HttpPost]
		public async Task<IActionResult> Remove(int id)
		{
			if (await reviewService.ReviewExistsAsync(id) == false)
			{
				return BadRequest();
			}

			if (await reviewService.HasReviewerWithIdAsync(id, User.Id()) == false && User.IsAdimn() == false)
			{
				return Unauthorized();
			}

			int carId = await reviewService.GetReviewCarIdAsync(id);

			await reviewService.RemoveReviewAsync(id);

			TempData["message"] = "Review successfully removed.";
			TempData["messageType"] = "success";

			return RedirectToAction(nameof(AllByCar), new { id = carId });
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			if (await reviewService.ReviewExistsAsync(id) == false)
			{
				return BadRequest();
			}

			if (await reviewService.HasReviewerWithIdAsync(id, User.Id()) == false && User.IsAdimn() == false)
			{
				return Unauthorized();
			}

			var model = await reviewService.CreateReviewFormViewModelByIdAsync(id);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, ReviewFormViewModel model)
		{
			if (await reviewService.ReviewExistsAsync(id) == false)
			{
				return BadRequest();
			}

			if (await reviewService.HasReviewerWithIdAsync(id, User.Id()) == false && User.IsAdimn() == false)
			{
				return Unauthorized();
			}

			await reviewService.EditAsync(id, model);

			TempData["message"] = "Review successfully edited.";
			TempData["messageType"] = "success";

			return RedirectToAction(nameof(AllByCar), new { id = model.CarId });
		}
	}
}
