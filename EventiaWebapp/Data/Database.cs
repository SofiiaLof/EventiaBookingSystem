using EventiaWebapp.Models;

namespace EventiaWebapp.Data
{
    public class Database
    {

        private readonly EventiaDbContext _ctx;

        public Database(EventiaDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Seed()
        {


            var organizer = new List<Organizer>
            {
                new() {Name ="Stockholms kommun", Email ="stocholm.k@gmail.com", Phone_number = "0745672345"},
                new() {Name ="Fotografiska", Email ="fotografiska@gmail.com", Phone_number = "0745672345"},
                new() {Name ="Ölbryggeri", Email ="stocholm.k@gmail.com", Phone_number = "0745672345"},
                new() {Name ="Vinkällaren AB", Email ="stocholm.k@gmail.com", Phone_number = "0745672345"},
            };

            await _ctx.AddRangeAsync(organizer);
            
            var events = new List<Event>
            {
                new (){Title = "Rally Sweden", Description = "Lorem ipsum", Place = "Stockholm", Adress = "Hötorget 12", Date = new DateTime(2022,04,05 ), Spots_available = 15, Organizer = organizer[0]},
                new (){Title = "Stand up", Description = "Lorem ipsum", Place = "Laugh house", Adress = "Västra Järnvägsgatan 20", Date = new DateTime(2022,05,11 ), Spots_available = 10, Organizer = organizer[0]},
                new (){Title = "Fotoutställning Vikingar", Description = "Lorem ipsum", Place = "Fotografiska", Adress = "Kungsgatan 12", Date = new DateTime(2022,04,05 ), Spots_available = 100, Organizer = organizer[1]},
                new (){Title = "Ölvandring", Description = "Lorem ipsum", Place = "Stockholm kommun", Adress = "Drottningatan 30", Date = new DateTime(2022,04,05 ), Spots_available = 25, Organizer = organizer[2]},
                new (){Title = "Vinprovning", Description = "Lorem ipsum", Place = "Restaurang Yin", Adress = "Rådmansgatan 23", Date = new DateTime(2022,04,05 ), Spots_available = 11, Organizer = organizer[3]},
                new (){Title = "Gröna Lung night ride", Description = "Lorem ipsum", Place = "Stockholm", Adress = "Nåtgatan 12", Date = new DateTime(2022,04,05 ), Spots_available = 50, Organizer = organizer[0]},
            };

            await _ctx.AddRangeAsync(events);

            var attendee = new List<Attendee>
            {
                new() {Name ="Sara Larsson", Email="saralars@gmail.com", Phone_number = "0723856745", Events = new List<Event>{events[0],events[1], events[2]}},
                new() {Name ="Olof Svensson", Email="olofsven@gmail.com", Phone_number = "0723856745", Events = new List<Event>{events[0]}},
                new() {Name ="Wilma Oskarsson", Email="wilmaosk@gmail.com", Phone_number = "0723856745", Events = new List<Event>{events[0]}},
                new() {Name ="Johan Johansson", Email="johanjohan@gmail.com", Phone_number = "0723856745", Events = new List<Event>{events[0]}},
                new() {Name ="Irma Ingvarsson", Email="irmaingvarsson@gmail.com", Phone_number = "0723856745", Events = new List<Event>{events[0]}},
            };

            await _ctx.AddRangeAsync(attendee);

            await _ctx.SaveChangesAsync();
        }

        public async Task CreateAndSeed()
        {

        }
    }
}
