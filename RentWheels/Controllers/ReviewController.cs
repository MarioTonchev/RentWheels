using Microsoft.AspNetCore.Mvc;

namespace RentWheels.Controllers
{
	public class ReviewController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
