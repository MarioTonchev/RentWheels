﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentWheels.Core.Contracts;
using RentWheels.Models;
using System.Diagnostics;

namespace RentWheels.Controllers
{
	public class HomeController : BaseController
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ICarService carService;

		public HomeController(ILogger<HomeController> logger
			,ICarService _carService)
		{
			_logger = logger;
			carService = _carService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var cars = await carService.LastThreeCarsAsync();

			return View(cars);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
