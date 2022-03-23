using EventiaWebapp.Models;
using EventiaWebapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    public class EventController : Controller
    {
        private readonly EventList _eventList;
        public EventController(EventList eventList)
        {
            _eventList = eventList;
        }
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

      
        public async Task<IActionResult> JoinEvent(int id)
        {
            var eventItem = await _eventList.GetEventItemById(id);

            return View(eventItem);
        }

        
        public async Task<IActionResult> Confirmation(int id)
        {
            var attendee = await _eventList.FindAttendee(1);
            var joinedEvent = await _eventList.JoinedEvent(attendee, id);

            return View(joinedEvent);
        }
    }
}
