using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Loan_Management.Models;

namespace Loan_Management.Controllers
{
    [Authorize]
    public class LoansController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public LoansController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Loans()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Finances()
        {
            return View();
        }

    }
}