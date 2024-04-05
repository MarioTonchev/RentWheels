using Microsoft.AspNetCore.Mvc;

namespace RentWheels.Areas.Admin.Controllers
{
	public class AdminHomeController : BaseAdminController
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
	}
}
