using EventiaWebapp.Data;
using EventiaWebapp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services
{
    public class OrganizerHandler
    {
        public readonly EventiaDbContext _ctx;

        public OrganizerHandler(EventiaDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Event>> GetOrganizerEventList(User organizer)
        {
            var query = _ctx.Users.Where(u => u.Id == organizer.Id);
            var userOrganizer = await query.FirstOrDefaultAsync();

            var exist = query.Any();

            if (!exist)
            {
                return null;
            }

            var queryEventList = _ctx.Events.Where(a => a.Organizer == userOrganizer);
                

            var eventList = await queryEventList.ToListAsync();

            return eventList;
           

        }

        public async Task AddEvent(User organizer, Event events)
        {
            var query = _ctx.Users.Where(u => u.Id == organizer.Id);
            var userOrganizer = await query.FirstOrDefaultAsync();

            var newEvent = new Event
            {
                Title = events.Title,
                Description = events.Description,
                Date = events.Date,
                Place = events.Place,
                Adress = events.Adress,
                Spots_available = events.Spots_available,
                    Organizer = userOrganizer
            };
            _ctx.Events.Add(newEvent);
            
            await _ctx.SaveChangesAsync();

        }
    }
}
