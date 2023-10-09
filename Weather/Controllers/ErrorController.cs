using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Weather.ViewModels;

namespace Weather.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            var errorViewModel = new ErrorViewModel { StatusCode = statusCode, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(errorViewModel);
        }
    }
}
