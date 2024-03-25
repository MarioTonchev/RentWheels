using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;
using RentWheels.Core.VeiwModels.Car;
using System.Security.Claims;

namespace RentWheels.Controllers
{
	public class CarController : BaseController
	{
		private readonly ICarService carService;
        private readonly IEngineService engineService;

        public CarController(ICarService _carService,
            IEngineService _engineService)
        {
            carService = _carService;
            engineService = _engineService;
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
            model.Categories = await carService.AllCategoriesNamesAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
		{
            var model = new CarFormViewModel()
            {
                Engines = await carService.AllEnginesFormAsync(),
                Categories = await carService.AllCategoriesFormAsync()
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

            if (!ModelState.IsValid)
            {
                model.Engines = await carService.AllEnginesFormAsync();
                model.Categories = await carService.AllCategoriesFormAsync();
                return View(model);
            }

            await carService.CreateAsync(model, User.Id());

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await carService.CarExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await carService.HasOwnerWithIdAsync(id, User.Id()) == false)
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

            if (await carService.HasOwnerWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            if (await engineService.EngineExistsAsync(model.EngineId) == false)
            {
                ModelState.AddModelError(nameof(model.EngineId), "Selected engine does not exist.");

                model.Engines = await carService.AllEnginesFormAsync();
                return View(model);
            }

            await carService.EditAsync(id, model);

            return RedirectToAction("MyLended", "Rental");
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

            if (await carService.HasOwnerWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            await carService.RemoveCarAsync(id);

            return RedirectToAction("MyLended", "Rental");
        }
    }
}
