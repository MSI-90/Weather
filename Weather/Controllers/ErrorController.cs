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

        [Route("Error/Message/{message}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ViewResult Error(string message)
        {
            var errorViewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(errorViewModel);
        }
    }
}
