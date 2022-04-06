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

            //var attendee = await _eventList.FindAttendee(1);
           // var attendeeEventList = await _eventList.GetAttendeeEventList(attendee);

            return View();
        }


        public async Task<IActionResult> JoinEvent(int id)
        {

            if (HttpContext.Request.Method == "POST")
            {
                //var attendee = await _eventList.FindAttendee(1);
               // var joinedEvent = await _eventList.JoinedEvent(attendee, id);

               // return View("JoinEvent", joinedEvent);

            }
            var eventItem = await _eventList.GetEventItemById(id);
            return View("JoinEvent", eventItem);

        }


    }
}
