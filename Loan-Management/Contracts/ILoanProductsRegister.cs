using Loan_Management.Models;

namespace Loan_Management.Contracts
{
    public interface ILoanProductsRegister
    {
        public Task<bool> CreateLoanProductsAsync(LoanProductsRegister loanProductsRegister, ApplicationUser user);

        public Task<LoanProductsRegister[]> GetAllRegisteredLoanProductsAsync();

        public Task<LoanProductsRegister[]> GetAllRegisteredLoanProductsByLoanIdAsync( Guid loanId);

        public Task<bool> ApplyForLoanAsync(LoanApplicationModel loanApplicationModel, ApplicationUser user);

        public Task<LoanApplicationModel[]> GetAllAppliedLoansPendingApprovalAsync();

        public Task<bool> ApproveAppliedLoans(Guid loanId);

    }
}