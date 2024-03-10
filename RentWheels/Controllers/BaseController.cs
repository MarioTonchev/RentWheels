using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentWheels.Controllers
{
	[Authorize]
	public class BaseController : Controller
	{
		
	}
}
