using EventiaWebapp.Data;
using EventiaWebapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services
{
    public class AdminList
    {

        private readonly EventiaDbContext _ctx;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminList(EventiaDbContext ctx, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _ctx = ctx;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var query = _ctx.Users;
            var allUsers = await query.ToListAsync();

         

            return allUsers;
        }

        public async Task<User> MakeAttendeAnOrganizer(string username)
        {
            var query = _ctx.Users.Where(u => u.UserName == username);

            var foundUser = await query.FirstOrDefaultAsync();
            if (!foundUser.IsOrganizer)
            {
                await _userManager.AddToRoleAsync(foundUser, "Organizer");
                foundUser.IsOrganizer = true;

            }

            _ctx.Update(foundUser);
          await  _ctx.SaveChangesAsync();
            return foundUser;
        }
    }
}
