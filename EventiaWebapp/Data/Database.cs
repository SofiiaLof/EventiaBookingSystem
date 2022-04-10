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

            var users = new List<User>
            {
                new(){UserName = "adminadmin@mail.com", First_name = "Albert", Last_name = "Kosivskij", Email = "adminadmin@mail.com"},
                new(){UserName = "attendee@mail.com", First_name = "Attendee", Last_name = "Attendee", Email = "attendee@mail.com"},
                new(){UserName = "organizer@mail.com", First_name = "Organizer", Last_name = "Organizer", Email = "organizer@mail.com"},

            };

            var events = new List<Event>
            {
                new (){Title = "Rally Sweden", Description = "Lorem ipsum", Place = "Stockholm", Adress = "Hötorget 12", Date = new DateTime(2022,04,05 ), Spots_available = 15, Organizer = users[2]},
                new (){Title = "Stand up", Description = "Lorem ipsum", Place = "Laugh house", Adress = "Västra Järnvägsgatan 20", Date = new DateTime(2022,05,11 ), Spots_available = 10,  Organizer = users[2]},
                new (){Title = "Fotoutställning Vikingar", Description = "Lorem ipsum", Place = "Fotografiska", Adress = "Kungsgatan 12", Date = new DateTime(2022,04,05 ), Spots_available = 100,  Organizer = users[2]},
                new (){Title = "Ölvandring", Description = "Lorem ipsum", Place = "Stockholm kommun", Adress = "Drottningatan 30", Date = new DateTime(2022,04,05 ), Spots_available = 25,  Organizer = users[2]},
                new (){Title = "Vinprovning", Description = "Lorem ipsum", Place = "Restaurang Yin", Adress = "Rådmansgatan 23", Date = new DateTime(2022,04,05 ), Spots_available = 11,  Organizer = users[2]},
                new (){Title = "Gröna Lund night ride", Description = "Lorem ipsum", Place = "Stockholm", Adress = "Nåtgatan 12", Date = new DateTime(2022,04,05 ), Spots_available = 50,  Organizer = users[2]},
            };

          

            //TODO Fixa layout till admin
            await _userManager.CreateAsync(users[0], "p@ssWord123");
            await _userManager.AddToRoleAsync(users[0], "Admin");

            await _userManager.CreateAsync(users[1], "p@ssworD1");
            await _userManager.AddToRoleAsync(users[1], "Attendee");

            await _userManager.CreateAsync(users[2], "p@ssworD2");
            await _userManager.AddToRoleAsync(users[2], "Organizer");

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
