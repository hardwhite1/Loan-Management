using Loan_Management.Models;

namespace Loan_Management.Contracts
{
    public interface ILoanProductsRegister
    {
        public Task<bool> CreateLoanProductsAsync(LoanProductsRegister loanProductsRegister, ApplicationUser user);

        public Task<LoanProductsRegister[]> GetAllRegisteredLoanProductsAsync(ApplicationUser user);

        public Task<LoanProductsRegister[]> GetAllRegisteredLoanProductsByLoanIdAsync(ApplicationUser user, Guid loanId);
    }
}