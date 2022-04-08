using EventiaWebapp.Models;
using EventiaWebapp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    public class EventController : Controller
    {
        private readonly EventList _eventList;
        private readonly UserManager<User> _userManager;
        public EventController(EventList eventList, UserManager<User> userManager)
        {
            _eventList = eventList;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Events()
        {
            return View();
        }
        public async Task<IActionResult> MyEvents()
        {
            var user = await _userManager.GetUserAsync(User);
            var attendeeEventList = await _eventList.GetAttendeeEventList(user);

            return View(attendeeEventList);
        }


        public async Task<IActionResult> JoinEvent(int id)
        {

            if (HttpContext.Request.Method == "POST")
            {
                var user = await _userManager.GetUserAsync(User);
                var joinedEvent = await _eventList.JoinedEvent(user, id);

               return View("JoinEvent", joinedEvent);

            }
            var eventItem = await _eventList.GetEventItemById(id);
            return View("JoinEvent", eventItem);

        }


    }
}
