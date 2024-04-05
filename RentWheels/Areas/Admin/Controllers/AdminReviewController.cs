using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;
using RentWheels.Core.ViewModels.Review;
using System.Security.Claims;

namespace RentWheels.Areas.Admin.Controllers
{
    public class AdminReviewController : BaseAdminController
    {
        private readonly IReviewService reviewService;
        private readonly ICarService carService;

        public AdminReviewController(
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

			return View(model);
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

			return RedirectToAction(nameof(AllByCar), new { id = model.CarId });
		}
	}
}
