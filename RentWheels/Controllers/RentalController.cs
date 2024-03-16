using Microsoft.AspNetCore.Mvc;

namespace RentWheels.Controllers
{
	public class RentalController : BaseController
	{
		public async Task<IActionResult> Rent()
		{
			return View();
		}
	}
}
