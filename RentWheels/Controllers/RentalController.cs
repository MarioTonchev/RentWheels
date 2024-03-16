using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;
using RentWheels.Core.VeiwModels.Rental;
using System.Globalization;
using System.Security.Claims;
using static RentWheels.Infrastructure.Constants.DataConstants;

namespace RentWheels.Controllers
{
	public class RentalController : BaseController
	{
		private readonly IRentalService rentalService;

        public RentalController(IRentalService _rentalService)
        {
			rentalService = _rentalService;
        }

		[HttpGet]
        public async Task<IActionResult> Rent(int id)
		{
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
            
            if (!DateTime.TryParseExact(model.Start, DateFormated, CultureInfo.InvariantCulture, DateTimeStyles.None
                , out e))
            {
                ModelState.AddModelError(nameof(model.End), $"Invalid date! Format must be: {DateFormated}");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await rentalService.RentCarAsync(model, id, User.Id(), s, e);

            return RedirectToAction("Mine");
        }

        //[HttpGet]
        //public async Task<IActionResult> Mine()
        //{
            
        //}
    }
}
