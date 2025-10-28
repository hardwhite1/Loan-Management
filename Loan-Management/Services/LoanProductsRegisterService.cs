using Loan_Management.Contracts;
using Loan_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Loan_Management.Services
{
    [Authorize(Roles = "Adminstrator, Finance")]
    public class LoanProductsRegisterService : ILoanProductsRegister
    {

        private readonly ApplicationDbContext _context;

        public LoanProductsRegisterService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> CreateLoanProductsAsync(LoanProductsRegister loanProductsRegister, ApplicationUser user)
        {
            if (loanProductsRegister == null) return false;

            // Check for existing loan products via loan Code
            var existingProduct = await _context.LoanProducts
                .FirstOrDefaultAsync(lp => lp.Code == loanProductsRegister.Code);
            if (existingProduct != null) return false; // Duplicate code found

            loanProductsRegister.Id = Guid.NewGuid();
            loanProductsRegister.UserId = user.Id;
            if (loanProductsRegister.PrincipalAmountMax <= loanProductsRegister.PrincipalAmountMin) return false;
            _context.LoanProducts.Add(loanProductsRegister);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<LoanProductsRegister[]> GetAllRegisteredLoanProductsAsync()
        {
            // return await _context.LoanProducts.Where(x => x.UserId == user.Id).ToArrayAsync();
            var items = await _context.LoanProducts.ToArrayAsync();
            return items;
        }

        public Task<LoanProductsRegister[]> GetAllRegisteredLoanProductsByLoanIdAsync( Guid loanId)
        {
            var items = _context.LoanProducts
                .Where(x =>  x.Id == loanId)
                .ToArrayAsync();
            return items;
        }

        public async Task<bool> ApplyForLoanAsync(LoanApplicationModel loanApplicationModel, ApplicationUser user)
        {
            if (loanApplicationModel == null) return false;

            // Validate the requested amount against the loan product's min and max
            var product = await _context.LoanProducts
                .FirstOrDefaultAsync(lp => lp.Id == loanApplicationModel.LoanProductId);
            if (product == null) return false; // Loan product not found

            if (loanApplicationModel.RequestedAmount < product.PrincipalAmountMin ||
                loanApplicationModel.RequestedAmount > product.PrincipalAmountMax)
            {
                return false; // Requested amount is out of bounds
            }

            // Set additional properties
            loanApplicationModel.Id = Guid.NewGuid();
            loanApplicationModel.UserId = user.Id;
            loanApplicationModel.LoanProduct = product;
            loanApplicationModel.RequiresCollateral = product.RequiresCollateral;
            loanApplicationModel.ApplicationDate = DateTimeOffset.Now;
            loanApplicationModel.MaturityDate = DateTimeOffset.Now.AddMonths(loanApplicationModel.RepaymentPeriodMonths);
            loanApplicationModel.ProcessedBy = "System"; // Remember to remove hardcoded value
            loanApplicationModel.Status = "Pending"; // Initial status

            _context.ApplicationModel.Add(loanApplicationModel);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<LoanApplicationModel[]> GetAllAppliedLoansPendingApprovalAsync() 
        {
            return await _context.ApplicationModel
            .Include(a => a.LoanProduct).Where(a => a.Status == "Pending")
            .ToArrayAsync();
        }

        public async Task<bool> ApproveAppliedLoansAsync(Guid loanId)
        {
            var loanApplication = await _context.ApplicationModel.FindAsync(loanId);
            if (loanApplication == null) return false;

            loanApplication.Status = "Approved";
            _context.ApplicationModel.Update(loanApplication);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> RejectAppliedLoansAsync(Guid loanId)
        {
            var appliedLoanProduct = await _context.ApplicationModel.FindAsync(loanId);
            if (appliedLoanProduct == null) return false;
            //set status to rejected
            appliedLoanProduct.Status = "Rejected";
            _context.ApplicationModel.Update(appliedLoanProduct);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<LoanApplicationModel[]> GetAllApprovedLoansAsync(ApplicationUser user)
        {
            return await _context.ApplicationModel
                .Include(a => a.LoanProduct)
                .Where(a => (a.Status == "Approved" || a.Status == "Rejected") && a.UserId == user.Id)
                .ToArrayAsync();
        }
    }
}