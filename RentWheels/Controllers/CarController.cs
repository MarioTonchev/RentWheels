using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;
using RentWheels.Core.VeiwModels.Car;
using RentWheels.Infrastructure.Models;
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
        public async Task<IActionResult> Add()
		{
            var model = new CarAddViewModel()
            {
                Engines = await carService.AllEnginesFormAsync()
            };

			return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> Add(CarAddViewModel model)
        {
            if (await carService.EngineExistsAsync(model.EngineId) == false)
            {
                ModelState.AddModelError(nameof(model.EngineId), "Selected engine does not exit!");
            }

            if (!ModelState.IsValid)
            {
                model.Engines = await carService.AllEnginesFormAsync();
                return View(model);
            }

            await carService.CreateAsync(model, User.Id());

            return RedirectToAction("Index", "Home");
        }
    }
}
