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
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();
            //perform a call to the service layer
            var successful = await _loanRegister.CreateLoanProductsAsync(loanProductsRegister, currentUser);
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
            var loanItems = await _loanRegister.GetAllRegisteredLoanProductsAsync(currentUser);
            var model = new LoanProductsViewModel
            {
                loanProducts = loanItems //loanProducts is a property defined in LoanProductsViewModel which(the property) is an array of LoanProductsRegister and will be passed to the view
            };
            return View(model);
        }
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> LoanDetails(Guid id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();

            var loan = await _loanRegister.GetAllRegisteredLoanProductsByLoanIdAsync(currentUser, id);
            if (loan == null || !loan.Any()) return NotFound();
            var model = new LoanProductsViewModel
            {
                loanProducts = loan
            };

            return View(model); // only one record should match
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