using System.Threading.Tasks.Dataflow;
using EventiaWebapp.Data;
using EventiaWebapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services
{
    public class AdminHandler
    {

        private readonly EventiaDbContext _ctx;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminHandler(EventiaDbContext ctx, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _ctx = ctx;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var query = _ctx.Users
                .Join(_ctx.UserRoles, u=>u.Id, r=>r.UserId, (u,r)=> new{user=u, userRole=r})
                .Join(_ctx.Roles, a=>a.userRole.RoleId, r=>r.Id, (a,r)=> new{user=a.user, role = r})
                .Where(r=>r.role.Name !="Admin").Select(ur=>ur.user).Distinct();


            var allUsers = await query.ToListAsync();

            return allUsers;
        }

        public async Task<User> MakeAttendeAnOrganizer(string id)
        {
            var query = _ctx.Users.Where(u => u.Id == id);

            var foundUser = await query.FirstOrDefaultAsync();
            if (!foundUser.IsOrganizer)
            {
                await _userManager.AddToRoleAsync(foundUser, "Organizer");
                await _userManager.RemoveFromRoleAsync(foundUser, "Attendee");
                foundUser.IsOrganizer = true;
                foundUser.BecomeAnOrganizer = false;


            }
            else
            {
                await _userManager.RemoveFromRoleAsync(foundUser, "Organizer");
                await _userManager.AddToRoleAsync(foundUser, "Attendee"); 
                foundUser.IsOrganizer = false;
            }

            _ctx.Update(foundUser);
            await  _ctx.SaveChangesAsync();
            return foundUser;
        }


       
    }
}
