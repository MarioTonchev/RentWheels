using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static RentWheels.Core.Constants.AdminRoleConstants;

namespace RentWheels.Areas.Admin.Controllers
{
	[Area(AdminAreaName)]
	[Authorize(Roles = AdminRole)]
	public class BaseAdminController : Controller
	{

	}
}
