using EventiaWebapp.Data;
using EventiaWebapp.Models;
using Microsoft.EntityFrameworkCore;

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
            var query = _ctx.Events;
            var eventsList = await query.ToListAsync();

            return eventsList;
        }
    }
}
