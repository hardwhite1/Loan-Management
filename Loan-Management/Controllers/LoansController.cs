using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Loan_Management.Models;
using Loan_Management.Contracts;
using System.Linq;

namespace Loan_Management.Controllers
{
    [Authorize]
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
            var loanItems = await _loanRegister.GetAllRegisteredLoanProductsAsync();
            var model = new LoanProductsViewModel
            {
                loanProducts = loanItems //loanProducts is a property defined in LoanProductsViewModel which(the property) is an array of LoanProductsRegister and will be passed to the view
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> LoanDetails(Guid id)
        {
            // var currentUser = await _userManager.GetUserAsync(User);
            // if (currentUser == null) return Unauthorized();

            var loan = await _loanRegister.GetAllRegisteredLoanProductsByLoanIdAsync(id);
            if (loan == null || !loan.Any()) return NotFound();
            var model = new LoanProductsViewModel
            {
                loanProducts = loan
            };

            return View(model); // only one record should match
        }

        [HttpGet]
        public async Task<IActionResult> LoanApplicationForm(Guid id) // loan product id
        {
            // var currentUser = await _userManager.GetUserAsync(User);
            // if (currentUser == null) return Unauthorized();

            // Get the loan product
            var loanProduct = await _loanRegister.GetAllRegisteredLoanProductsByLoanIdAsync(id);
            if (loanProduct == null || !loanProduct.Any()) return NotFound();

            var product = loanProduct.First();

            // Set ViewBag property
            ViewBag.LoanProductName = product.Name;
            ViewBag.LoanProductInterestRate = product.InterestRate;

            // Build the application model
            var model = new LoanApplicationModel
            {
                LoanProductId = product.Id,
                RequiresCollateral = product.RequiresCollateral,
                ApplicationDate = DateTimeOffset.Now,
                MaturityDate = DateTimeOffset.Now.AddMonths(1),
                ProcessingFee = 0.02m * product.PrincipalAmountMin,
                ProcessedBy = "System" //Remember to remove hardcoded value
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LoanApplicationFormRegister(LoanApplicationModel loanApplicationModel)
        {
            if (!ModelState.IsValid)
            {
                foreach (var (key, error) in from key in ModelState.Keys
                                             let state = ModelState[key]
                                             from error in state.Errors
                                             select (key, error))
                {
                    Console.WriteLine($"key: {key}, Error: {error.ErrorMessage}");
                }
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();
            var successful = await _loanRegister.ApplyForLoanAsync(loanApplicationModel, currentUser);
            if (!successful)
            {
                return BadRequest("Could not apply for loan product");
            }
            return RedirectToAction("Finances");

        }
        [HttpGet]
        public async Task<IActionResult> Finances()
        {
            var appliedLoans = await _loanRegister.GetAllAppliedLoansPendingApprovalAsync();

            //set ViewBag properties if needed
            ViewBag.LoanProductName = appliedLoans.FirstOrDefault()?.LoanProduct?.Name ?? "N/A";

            var model = new LoanApplicationViewModel
            {
                loanApplicationModel = appliedLoans      
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveLoan(Guid loanId)
        {
            var successful = await _loanRegister.ApproveAppliedLoansAsync(loanId);
            if (!successful)
            {
                return BadRequest("Could not approve loan");
            }

            // TempData["Success"] = $"Loan for {loan.FirstName} {loan.LastName} has been approved.";
            return RedirectToAction(nameof(Loans));
        }

        // [HttpPost]
        // public async Task<IActionResult> RejectLoan(Guid id)
        // {
        //     var loan = await _context.LoanApplications.FindAsync(id);
        //     if (loan == null) return NotFound();

        //     loan.Status = "Rejected";
        //     loan.ProcessedBy = User.Identity?.Name;
        //     await _context.SaveChangesAsync();

        //     TempData["Error"] = $"Loan for {loan.FirstName} {loan.LastName} has been rejected.";
        //     return RedirectToAction(nameof(Finances));
        // }

    }
}