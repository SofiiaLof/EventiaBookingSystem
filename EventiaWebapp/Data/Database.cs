using EventiaWebapp.Models;
using Microsoft.AspNetCore.Identity;

namespace EventiaWebapp.Data
{
    public class Database
    {

        private readonly EventiaDbContext _ctx;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public Database(EventiaDbContext ctx, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _ctx = ctx;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        public async Task Seed()
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Attendee"));
            await _roleManager.CreateAsync(new IdentityRole("Organizer"));


            var events = new List<Event>
            {
                new (){Title = "Rally Sweden", Description = "Lorem ipsum", Place = "Stockholm", Adress = "Hötorget 12", Date = new DateTime(2022,04,05 ), Spots_available = 15},
                new (){Title = "Stand up", Description = "Lorem ipsum", Place = "Laugh house", Adress = "Västra Järnvägsgatan 20", Date = new DateTime(2022,05,11 ), Spots_available = 10},
                new (){Title = "Fotoutställning Vikingar", Description = "Lorem ipsum", Place = "Fotografiska", Adress = "Kungsgatan 12", Date = new DateTime(2022,04,05 ), Spots_available = 100},
                new (){Title = "Ölvandring", Description = "Lorem ipsum", Place = "Stockholm kommun", Adress = "Drottningatan 30", Date = new DateTime(2022,04,05 ), Spots_available = 25},
                new (){Title = "Vinprovning", Description = "Lorem ipsum", Place = "Restaurang Yin", Adress = "Rådmansgatan 23", Date = new DateTime(2022,04,05 ), Spots_available = 11},
                new (){Title = "Gröna Lund night ride", Description = "Lorem ipsum", Place = "Stockholm", Adress = "Nåtgatan 12", Date = new DateTime(2022,04,05 ), Spots_available = 50},
            };

            var admin = new List<User>
            {
               new(){UserName = "adminadmin@mail.com", First_name = "Albert", Last_name = "Kosivskij", Email = "adminadmin@mail.com"}
            };
            //TODO Fixa layout till admin
            await _userManager.CreateAsync(admin[0], "p@ssWord123");
            await _userManager.AddToRoleAsync(admin[0], "Admin");

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
