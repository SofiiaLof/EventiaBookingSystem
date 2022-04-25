using EventiaWebapp.Models;
using EventiaWebapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EventHandler = EventiaWebapp.Services.EventHandler;

namespace EventiaWebapp.Controllers
{
    public class EventController : Controller
    {
        private readonly EventHandler _eventHandler;
        private readonly UserManager<User> _userManager;
        private readonly OrganizerHandler _organizerHandler;
       
        public EventController(EventHandler eventHandler, UserManager<User> userManager, OrganizerHandler organizerHandler)
        {
            _eventHandler = eventHandler;
            _userManager = userManager;
            _organizerHandler = organizerHandler;
           
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
            var attendeeEventList = await _eventHandler.GetAttendeeEventList(user);

            return View(attendeeEventList);
        }


        public async Task<IActionResult> JoinEvent(int id)
        {

            if (HttpContext.Request.Method == "POST")
            {
                var user = await _userManager.GetUserAsync(User);
                var joinedEvent = await _eventHandler.JoinedEvent(user, id);

               return View("JoinEvent", joinedEvent);

            }
            var eventItem = await _eventHandler.GetEventItemById(id);
            return View("JoinEvent", eventItem);

        }

        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> AddEvent()
        {
            return View();

        }

        [Authorize(Roles = "Organizer")]
        [HttpPost]
        public async Task<IActionResult> AddEvent( [Bind("Id,Title, Description, Date, Place, Adress, Spots_avaliable")] Event events)
        {
           
                var user = await _userManager.GetUserAsync(User);
               await _organizerHandler.AddEvent(user,events);

               return RedirectToAction("OrganizeEvents");
            
             
        }

        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> OrganizeEvents()
        {

            var user = await _userManager.GetUserAsync(User);
            var organizerList = await _organizerHandler.GetOrganizerEventList(user);
            return View(organizerList);
        }

        public async Task<IActionResult> RequestToMakeUserAnOrganizer()
        {
            var user = await _userManager.GetUserAsync(User);
            await _eventHandler.MakeChangeRequest(user);
            return View(user);
        }
    
    }
}
