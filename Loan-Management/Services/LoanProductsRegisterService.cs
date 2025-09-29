using Loan_Management.Contracts;
using Loan_Management.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Loan_Management.Services
{
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

        public async Task<LoanProductsRegister[]> GetAllRegisteredLoanProductsAsync(ApplicationUser user)
        {
            // return await _context.LoanProducts.Where(x => x.UserId == user.Id).ToArrayAsync();
            var items = await _context.LoanProducts.Where(x => x.UserId == user.Id).ToArrayAsync();
            return items;
        }

        public Task<LoanProductsRegister[]> GetAllRegisteredLoanProductsByLoanIdAsync(ApplicationUser user, Guid loanId)
        {
            var items = _context.LoanProducts
                .Where(x => x.UserId == user.Id && x.Id == loanId)
                .ToArrayAsync();
            return items;
        }
    }
}