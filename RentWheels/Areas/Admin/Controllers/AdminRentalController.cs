using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;
using RentWheels.Core.ViewModels.Rental;
using System.Globalization;
using System.Security.Claims;
using static RentWheels.Infrastructure.Constants.DataConstants;

namespace RentWheels.Areas.Admin.Controllers
{
    public class AdminRentalController : BaseAdminController
	{
		private readonly IRentalService rentalService;

        public AdminRentalController(IRentalService _rentalService)
        {
			rentalService = _rentalService;
        }

        [HttpGet]
		public async Task<IActionResult> All()
		{
			var model = await rentalService.AllRentals();

			return View(model);
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

            return RedirectToAction(nameof(All));
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

            return RedirectToAction(nameof(All));
        }
    }
}
