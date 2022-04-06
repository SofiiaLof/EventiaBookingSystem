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
            
            
            
            var events = new List<Event>
            {
                new (){Title = "Rally Sweden", Description = "Lorem ipsum", Place = "Stockholm", Adress = "Hötorget 12", Date = new DateTime(2022,04,05 ), Spots_available = 15},
                new (){Title = "Stand up", Description = "Lorem ipsum", Place = "Laugh house", Adress = "Västra Järnvägsgatan 20", Date = new DateTime(2022,05,11 ), Spots_available = 10},
                new (){Title = "Fotoutställning Vikingar", Description = "Lorem ipsum", Place = "Fotografiska", Adress = "Kungsgatan 12", Date = new DateTime(2022,04,05 ), Spots_available = 100},
                new (){Title = "Ölvandring", Description = "Lorem ipsum", Place = "Stockholm kommun", Adress = "Drottningatan 30", Date = new DateTime(2022,04,05 ), Spots_available = 25},
                new (){Title = "Vinprovning", Description = "Lorem ipsum", Place = "Restaurang Yin", Adress = "Rådmansgatan 23", Date = new DateTime(2022,04,05 ), Spots_available = 11},
                new (){Title = "Gröna Lund night ride", Description = "Lorem ipsum", Place = "Stockholm", Adress = "Nåtgatan 12", Date = new DateTime(2022,04,05 ), Spots_available = 50},
            };

            await _ctx.AddRangeAsync(events);


            await _ctx.SaveChangesAsync();
        }

        public async Task CreateAndSeed()
        {
            bool ifNotExist = await _ctx.Database.EnsureCreatedAsync();
            if (ifNotExist)
            {
                await Seed();
            }
        }
    }
}
