using EventiaWebapp.Models;
using EventiaWebapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    public class EventController : Controller
    {
        private readonly EventList _eventList;
        private readonly UserManager<User> _userManager;
        private readonly OrganizerList _organizerList;
       
        public EventController(EventList eventList, UserManager<User> userManager, OrganizerList organizerList)
        {
            _eventList = eventList;
            _userManager = userManager;
            _organizerList = organizerList;
           
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [AllowAnonymous]
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

        public async Task<IActionResult> AddEvent()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddEvent( [Bind("Id,Title, Description, Date, Place, Adress, Spots_avaliable")] Event events)
        {
           
                var user = await _userManager.GetUserAsync(User);
               await _organizerList.AddEvent(user,events);

               return View();
            
             
        }

        public async Task<IActionResult> OrganizeEvents()
        {

            var user = await _userManager.GetUserAsync(User);
            var organizerList = await _organizerList.GetOrganizerEventList(user);
            return View(organizerList);
        }


    
    }
}
