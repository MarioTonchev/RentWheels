using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;
using RentWheels.Core.Services;
using RentWheels.Core.ViewModels.Car;
using System.Security.Claims;

namespace RentWheels.Areas.Admin.Controllers
{
	public class AdminCarController : BaseAdminController
	{
		private readonly ICarService carService;
		private readonly IEngineService engineService;
		private readonly ICategoryService categoryService;

        public AdminCarController(
			ICarService _carService,
			IEngineService _engineService,
			ICategoryService _categoryService)
        {
            carService = _carService;
			engineService = _engineService;
			categoryService = _categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await carService.AllCarsAsync();

            return View(model);
        }

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			if (await carService.CarExistsAsync(id) == false)
			{
				return BadRequest();
			}

			if (await carService.HasOwnerWithIdAsync(id, User.Id()) == false && User.IsAdimn() == false)
			{
				return Unauthorized();
			}

			var model = await carService.CreateCarFormViewModelByIdAsync(id);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, CarFormViewModel model)
		{
			if (await carService.CarExistsAsync(id) == false)
			{
				return BadRequest();
			}

			if (await carService.HasOwnerWithIdAsync(id, User.Id()) == false && User.IsAdimn() == false)
			{
				return Unauthorized();
			}

			if (await engineService.EngineExistsAsync(model.EngineId) == false)
			{
				ModelState.AddModelError(nameof(model.EngineId), "Selected engine does not exist.");
			}

			if (await categoryService.CategoryExistsAsync(model.CategoryId) == false)
			{
				ModelState.AddModelError(nameof(model.CategoryId), "Selected category does not exist.");
			}

			if (!ModelState.IsValid)
			{
				model.Engines = await engineService.AllEnginesFormAsync();
				model.Categories = await categoryService.AllCategoriesFormAsync();

				return View(model);
			}

			await carService.EditAsync(id, model);

			return RedirectToAction(nameof(All));
		}

		[HttpPost]
		public async Task<IActionResult> Remove(int id)
		{
			if (await carService.CarExistsAsync(id) == false)
			{
				return BadRequest();
			}

			if (await carService.HasOwnerWithIdAsync(id, User.Id()) == false && User.IsAdimn() == false)
			{
				return Unauthorized();
			}

			await carService.RemoveCarAsync(id);

			return RedirectToAction(nameof(All));
		}
	}
}
