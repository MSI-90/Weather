using Microsoft.AspNetCore.Mvc;
using Weather.ViewModels;

namespace Weather.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            var errorViewModel = new ErrorViewModel { StatusCode = statusCode };
            return View(errorViewModel);
        }
    }
}
