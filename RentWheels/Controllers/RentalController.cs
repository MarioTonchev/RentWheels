using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;
using RentWheels.Core.ViewModels.Rental;
using System.Globalization;
using System.Security.Claims;
using static RentWheels.Infrastructure.Constants.DataConstants;

namespace RentWheels.Controllers
{
    public class RentalController : BaseController
    {
        private readonly IRentalService rentalService;
        private readonly ICarService carService;

        public RentalController(
            IRentalService _rentalService, 
            ICarService _carService)
        {
            rentalService = _rentalService;
            carService = _carService;

        }

        [HttpGet]
        public async Task<IActionResult> Rent(int id)
        {
            if (await carService.CarExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await rentalService.IsCarValidForRentAsync(id, User.Id()) == false)
            {
                return RedirectToAction("All", "Car");
            }

            var model = new RentCarFormViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id, RentCarFormViewModel model)
        {
            DateTime s = DateTime.Now;
            DateTime e = DateTime.Now;

            if (!DateTime.TryParseExact(model.Start, DateFormated, CultureInfo.InvariantCulture, DateTimeStyles.None
                , out s))
            {
                ModelState.AddModelError(nameof(model.Start), $"Invalid date! Format must be: {DateFormated}");
            }

            if (!DateTime.TryParseExact(model.End, DateFormated, CultureInfo.InvariantCulture, DateTimeStyles.None
                , out e))
            {
                ModelState.AddModelError(nameof(model.End), $"Invalid date! Format must be: {DateFormated}");
            }

            if (s > e)
            {
				ModelState.AddModelError(nameof(model.Start), $"Start date cannot be greater than the end date!");
			}

            if (!ModelState.IsValid)
            {
                return View(model);
            }
          
            await rentalService.RentCarAsync(model, id, User.Id(), s, e);
            
            return RedirectToAction(nameof(MyRented));
        }

        [HttpGet]
        public async Task<IActionResult> MyRented()
        {
            var cars = await rentalService.MyRentedCarsAsync(User.Id());

            return View(cars);
        }

        [HttpPost]
        public async Task<IActionResult> End(int id)
        {
            if (await rentalService.RentalExistsAsync(id) == false)
            {
                return BadRequest();
            }

			if (await rentalService.HasRenterWithIdAsync(id, User.Id()) == false && User.IsAdimn() == false)
			{
				return Unauthorized();
			}

			await rentalService.EndRentAsync(id);

            return RedirectToAction(nameof(MyRented));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await rentalService.RentalExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await rentalService.HasRenterWithIdAsync(id, User.Id()) == false && User.IsAdimn() == false)
            {
                return Unauthorized();
            }

            var model = await rentalService.CreateRentalFormViewModelByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RentCarFormViewModel model)
        {
			if (await rentalService.RentalExistsAsync(id) == false)
			{
				return BadRequest();
			}

			if (await rentalService.HasRenterWithIdAsync(id, User.Id()) == false && User.IsAdimn() == false)
			{
				return Unauthorized();
			}

			DateTime s = DateTime.Now;
			DateTime e = DateTime.Now;

			if (!DateTime.TryParseExact(model.Start, DateFormated, CultureInfo.InvariantCulture, DateTimeStyles.None
				, out s))
			{
				ModelState.AddModelError(nameof(model.Start), $"Invalid date! Format must be: {DateFormated}");
			}

			if (!DateTime.TryParseExact(model.End, DateFormated, CultureInfo.InvariantCulture, DateTimeStyles.None
				, out e))
			{
				ModelState.AddModelError(nameof(model.End), $"Invalid date! Format must be: {DateFormated}");
			}

			if (s > e)
			{
				ModelState.AddModelError(nameof(model.Start), $"Start date cannot be greater than the end date!");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await rentalService.EditAsync(id, model, s, e);

            return RedirectToAction(nameof(MyRented));
		}

        [HttpGet]
        public async Task<IActionResult> MyRentHistory()
        {
            var cars = await rentalService.MyRentHistoryAsync(User.Id());

            return View(cars); 
        }
    }
}
