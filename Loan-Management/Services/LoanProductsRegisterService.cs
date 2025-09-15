using Loan_Management.Contracts;
using Loan_Management.Models;
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
       

        public async Task<bool> CreateLoanProductsAsync(LoanProductsRegister loanProductsRegister)
        {
            if (loanProductsRegister == null) return false;

            // Check for existing loan products via loan code
            var existingProduct = await _context.LoanProducts
                .FirstOrDefaultAsync(lp => lp.Code == loanProductsRegister.Code);
            if (existingProduct != null) return false; // Duplicate code found

            loanProductsRegister.Id = Guid.NewGuid();
            loanProductsRegister.CreatedDate = DateTime.UtcNow;
            loanProductsRegister.LastUpdated = DateTime.UtcNow;

            _context.LoanProducts.Add(loanProductsRegister);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }
    }
}