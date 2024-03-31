using Microsoft.AspNetCore.Mvc;

namespace RentWheels.Controllers
{
	public class ReviewController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> All()
		{
			return View();
		}
	}
}
