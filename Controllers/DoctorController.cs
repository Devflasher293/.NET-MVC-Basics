

using Microsoft.AspNetCore.Mvc;
using ViewsControllers.Models;

namespace ViewsControllers.Controllers
{
    public class DoctorController : Controller
    {
        [HttpGet("/FeverCheck")]
        public IActionResult FeverCheck()
        {
            return View();
        }

        [HttpPost("/FeverCheck")]
        public IActionResult FeverCheck(float temperature, string scale = "Celsius")
        {
            string message = TemperatureChecker.CheckTemperature(temperature, scale);
            ViewData["Message"] = message;
            return View();
        }
    }
}
