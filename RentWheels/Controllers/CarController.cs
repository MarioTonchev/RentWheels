using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;
using RentWheels.Core.VeiwModels.Car;
using System.Security.Claims;

namespace RentWheels.Controllers
{
	public class CarController : BaseController
	{
		private ICarService carService;

        public CarController(ICarService _carService)
        {
            carService = _carService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var cars = await carService.AllCarsAsync();

            return View(cars);
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
            if (await carService.EngineExistsAsync(model.EngineId) == false)
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

            if (await carService.EngineExistsAsync(model.EngineId) == false)
            {
                ModelState.AddModelError(nameof(model.EngineId), "Selected engine does not exist.");

                model.Engines = await carService.AllEnginesFormAsync();
                return View(model);
            }

            await carService.EditAsync(id, model);

            //To do: Redirect to other page.
            return RedirectToAction("Index", "Home");
        }
    }
}
