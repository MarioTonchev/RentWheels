using Microsoft.AspNetCore.Mvc;

namespace RentWheels.Controllers
{
	public class RentalController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
