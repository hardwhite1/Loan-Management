using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Loan_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Loan_Management.Controllers
{
    [Authorize(Roles ="Adminstrator")]
    public class UsersController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        //constructor
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = (await _userManager.GetUsersInRoleAsync("Adminstrator")).ToArray();

            var everyone = await _userManager.Users.ToArrayAsync();

            var usersModel = new UsersViewModel
            {
                Adminstrators = admins,
                Everyone = everyone
            };

            return View(usersModel);
        }

    }
}