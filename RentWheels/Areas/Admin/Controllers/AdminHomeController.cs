using Microsoft.AspNetCore.Mvc;

namespace RentWheels.Areas.Admin.Controllers
{
	public class AdminHomeController : BaseAdminController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
