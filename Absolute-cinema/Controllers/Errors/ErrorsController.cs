using Microsoft.AspNetCore.Mvc;

namespace Absolute_cinema.Controllers.Errors
{
    public class ErrorsController : Controller
    {
        [Route("Errors/{statusCode}")]
        public IActionResult HandleError(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("Errors/NotFound");
                case 403:
                    return View("Errors/Forbidden");
                case 500:
                    return View("Errors/ServerError");
                default:
                    return View("Error");
            }
        }
    }
}
