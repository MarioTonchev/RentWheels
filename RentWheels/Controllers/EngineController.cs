using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;

namespace RentWheels.Controllers
{
    public class EngineController : BaseController
    {
        private readonly IEngineService engineService;

        public EngineController(IEngineService _engineService)
        {
            engineService = _engineService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (await engineService.EngineExistsAsync(id) == false)
            {
                return BadRequest();
            }

            var model = await engineService.DetailsAsync(id);

            return View(model);
        }
    }
}
