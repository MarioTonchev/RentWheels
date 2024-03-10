using Microsoft.AspNetCore.Mvc;

namespace RentWheels.Controllers
{
	public class CarController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
