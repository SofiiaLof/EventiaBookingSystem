using EventiaWebapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{

    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly AdminHandler _adminHandler;

        public AdminController(AdminHandler adminHandler)
        {
            _adminHandler = adminHandler;
        }


        public async Task<IActionResult> ManageUsers()
        {
            var userList = await _adminHandler.GetAllUsers();
            return View(userList);
        }



       [HttpPost]
        public async Task<IActionResult> ManageUsers(string id)
        {
           await _adminHandler.MakeAttendeAnOrganizer(id);

            
         
            return RedirectToAction("ManageUsers");
            
        }

    }
}
