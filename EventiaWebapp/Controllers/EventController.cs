using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Events()
        {
            return View();
        }
        public IActionResult MyEvents()
        {
            return View();
        }
    }
}
