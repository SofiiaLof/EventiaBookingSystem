using EventiaWebapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
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
    }
}
