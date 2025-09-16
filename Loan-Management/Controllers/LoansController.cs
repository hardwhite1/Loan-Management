using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Loan_Management.Models;
using Loan_Management.Contracts;

namespace Loan_Management.Controllers
{
    [Authorize(Roles = "Adminstrator")]
    public class LoansController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILoanProductsRegister _loanRegister;

        public LoansController(UserManager<ApplicationUser> userManager, ILoanProductsRegister loanRegister)
        {
            _userManager = userManager;
            _loanRegister = loanRegister;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult LoanProductsRegisterForm()
        {
            return View();
        }
        //Create an action method that processes loan registration
        [HttpPost]
        public async Task<IActionResult> LoanProductsRegister(LoanProductsRegister loanProductsRegister)
        {
            //Model validation check
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"key: {key}, Error: {error.ErrorMessage}");
                    }
                }
            }
            //perform a call to the service layer
            var successful = await _loanRegister.CreateLoanProductsAsync(loanProductsRegister);
            if (!successful)
            {
                return BadRequest("Could not add item");
            }
            return RedirectToAction("Loans");

        }
        [HttpGet]
        public async Task<IActionResult> Loans()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();
            return View();
        }

        [HttpGet]
        public IActionResult Users()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Finances()
        {
            return View();
        }

    }
}