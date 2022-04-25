using EventiaWebapp.Data;
using EventiaWebapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace EventiaWebapp.Services
{
    public class EventHandler
    {

        public readonly EventiaDbContext _ctx;

        public EventHandler(EventiaDbContext ctx)
        {
            _ctx = ctx;
        }
        
        public async Task<List<Event>> GetEventList()
        {
            var query = _ctx.Events.Include(o => o.Organizer);

            var eventsList = await query.ToListAsync();
            var exist = eventsList.Any();

            if (!exist)
            {
                return null;
            }
            return eventsList;

        }

        public async Task<Event> GetEventItemById(int id)
        {
            var query = _ctx.Events.Where(e => e.Id == id)
                .Include(o => o.Organizer);
            var eventItem = await query.FirstOrDefaultAsync();

            var exist = query.Any();

            if (!exist)
            {
                return null;
            }

            return eventItem;



        }

        public async Task<Event> JoinedEvent(User attendee, int eventId)
        {
            var queryAttendee = _ctx.Users.Where(a => a.Id == attendee.Id).Include(e => e.JoinedEvents);
            var attendeeFound = await queryAttendee.FirstOrDefaultAsync();

            var queryEvent = _ctx.Events.Where(e => e.Id == eventId)
                .Include(o => o.Organizer);

            var eventFound = await queryEvent.FirstOrDefaultAsync();
            var eventExist = queryEvent.Any();
            var attendeeExist = queryAttendee.Any();

            if (!eventExist || !attendeeExist)
            {
                return null;
            }
            attendeeFound.JoinedEvents.Add(eventFound);


            _ctx.Update(attendeeFound);
            await _ctx.SaveChangesAsync();

            return eventFound;
        }

       
        public async Task<List<Event>> GetAttendeeEventList(User attendee)
        {
            var queryAttendee = _ctx.Users.Where(a => a.Id == attendee.Id);

            var attendeeFound = await queryAttendee.FirstOrDefaultAsync();

            var exist = queryAttendee.Any();

            if (!exist)
            {
                return null;     
            }

            var queryEventList = _ctx.Events.Where(a => a.Attendees.Contains(attendeeFound))
                .Include(o => o.Organizer);

            var eventList = await queryEventList.ToListAsync();

            return eventList;


        }

        public async Task MakeChangeRequest(User user)
        {
            var queryAttendee = _ctx.Users.Where(a => a.Id == user.Id);
            var attendeeFound = await queryAttendee.FirstOrDefaultAsync();

            attendeeFound.BecomeAnOrganizer = true;
            _ctx.Update(attendeeFound);
            await _ctx.SaveChangesAsync();
        }
       
    }
        
}
