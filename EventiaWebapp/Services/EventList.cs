using EventiaWebapp.Data;
using EventiaWebapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace EventiaWebapp.Services
{
    public class EventList
    {

        public readonly EventiaDbContext _ctx;

        public EventList(EventiaDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task <List<Event>> GetEventList()
        {
            var query = _ctx.Events.Include(o=>o.Organizer);
            var eventsList = await query.ToListAsync();

            return eventsList;
        }

        public async Task<Event> GetEventItemById(int id)
        {
            var query = _ctx.Events.Where(e => e.Id == id)
                .Include(o=>o.Organizer);
            var eventItem = await query.FirstOrDefaultAsync();

            return eventItem;

        }
        
        public async Task<Event> JoinedEvent(Attendee attendee, int eventId)
        {
            var queryAttendee = _ctx.Attendes.Where(a => a.Id == attendee.Id).Include(e=>e.Events);
            var attendeeFound = await queryAttendee.FirstOrDefaultAsync();

            var queryEvent = _ctx.Events.Where(e => e.Id == eventId)
                .Include(o=>o.Organizer);
            var eventFound = await queryEvent.FirstOrDefaultAsync();

            attendeeFound.Events.Add(eventFound);
      

          _ctx.Update(attendeeFound);
          await  _ctx.SaveChangesAsync();

            return  eventFound;
        }

        public async Task<Attendee> FindAttendee(int id)
        {
            var query = await _ctx.Attendes.Where(a => a.Id == id).FirstOrDefaultAsync();

            return query;
        }

        public async Task<List<Event>> GetAttendeeEventList(Attendee attendee)
        {
            var queryAttendee = _ctx.Attendes.Where(a => a.Id == attendee.Id)
                .Include(e => e.Events);
                
            var attendeeFound = await queryAttendee.FirstOrDefaultAsync();

            var queryEventList = _ctx.Events.Where(a => a.Attendees.Contains(attendeeFound)).Include(o => o.Organizer);
            var eventList = await queryEventList.ToListAsync();

            return eventList;


        }
    }
}
