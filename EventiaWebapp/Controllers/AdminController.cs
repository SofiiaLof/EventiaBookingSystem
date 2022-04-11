using EventiaWebapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{

    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly AdminList _adminList;

        public AdminController(AdminList adminList)
        {
            _adminList = adminList;
        }


        public async Task<IActionResult> ManageUsers()
        {
            var userList = await _adminList.GetAllUsers();
            return View(userList);
        }



       [HttpPost]
        public async Task<IActionResult> ManageUsers(string id)
        {
           await _adminList.MakeAttendeAnOrganizer(id);

            var userList = await _adminList.GetAllUsers();
            return View(userList);
        }



    }
}
