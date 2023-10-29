using Microsoft.AspNetCore.Mvc;
using Weather.ViewModels;

namespace Weather.Controllers
{
    
    public class StatusCodeController : Controller
    {
        [Route("/status/{statusCode}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ViewResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("404");
            }
            return View(new ErrorViewModel { StatusCode = statusCode });
        }
    }
}
