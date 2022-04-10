using EventiaWebapp.Data;
using EventiaWebapp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Services
{
    public class AdminList
    {

        public readonly EventiaDbContext _ctx;

        public AdminList(EventiaDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var query = _ctx.Users;
            var allUsers = await query.ToListAsync();

         

            return allUsers;
        }
    }
}
