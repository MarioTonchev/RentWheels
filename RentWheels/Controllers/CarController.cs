using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;
using RentWheels.Core.ViewModels.Car;
using System.Security.Claims;

namespace RentWheels.Controllers
{
	public class CarController : BaseController
	{
		private readonly ICarService carService;
        private readonly IEngineService engineService;
        private readonly ICategoryService categoryService;

        public CarController(ICarService _carService,
            IEngineService _engineService,
            ICategoryService _categoryService)
        {
            carService = _carService;
            engineService = _engineService;
            categoryService = _categoryService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllCarsQueryViewModel model)
        {
            var cars = await carService.AllAsync(
                model.Category,
                model.SearchTerm,
                model.Sorting,
                model.CurrentPage,
                model.CarsPerPage);

            model.TotalCarsCount = cars.TotalCarsCount;
            model.Cars = cars.Cars;
            model.TotalPages = cars.TotalPages;
            model.Categories = await categoryService.AllCategoriesNamesAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
		{
            var model = new CarFormViewModel()
            {
                Engines = await engineService.AllEnginesFormAsync(),
                Categories = await categoryService.AllCategoriesFormAsync()
            };

			return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> Add(CarFormViewModel model)
        {
            if (await engineService.EngineExistsAsync(model.EngineId) == false)
            {
                ModelState.AddModelError(nameof(model.EngineId), "Selected engine does not exit!");
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

            await carService.CreateAsync(model, User.Id());

            return RedirectToAction(nameof(All));
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

            return RedirectToAction(nameof(MyCars));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (await carService.CarExistsAsync(id) == false)
            {
                return BadRequest();
            }

            var model = await carService.DetailsAsync(id);

            return View(model);
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

            return RedirectToAction(nameof(MyCars));
        }

		[HttpGet]
		public async Task<IActionResult> MyCars()
		{
			var model = await carService.MyCarsAsync(User.Id());

			return View(model);
		}
	}
}
