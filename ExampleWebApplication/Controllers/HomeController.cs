using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ExampleForStudents.Contracts;
using ExampleWebApplication.HttpClients;
using ExampleWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExampleWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExampleApiClient _client;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IExampleApiClient client)
        {
            _logger = logger;
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<ActionResult<IEnumerable<CarDto>>> Cars()
        {
            var result = await _client.GetCarsByFilter(new CarsSearchFilterDto());
            if (!result.Success)
                // return some user friendly error message here
                return new ObjectResult(result.Errors) { StatusCode = StatusCodes.Status400BadRequest };

            return new OkObjectResult(result.Data);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}