using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBS.Web.Constants
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 401:
                    ViewBag.ErrorMessage = $"{statusCode} Unauthorized!";
                    break;
                case 400:
                    ViewBag.ErrorMessage = $"{statusCode} Bad Request!";
                    break;
                case 403:
                    ViewBag.ErrorMessage = $"{statusCode} Forbidden!";
                    break;
                case 404:
                    ViewBag.ErrorMessage = $"{statusCode} Not Found!";
                    break;
                case 408:
                    ViewBag.ErrorMessage = $"{statusCode} Request Timeout";
                    break;
                default:
                    ViewBag.ErrorMessage = $"500 Internal error";
                    break;
            }
            return View("Error");
        }
    }
}