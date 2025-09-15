using Loan_Management.Models;

namespace Loan_Management.Contracts
{
    public interface ILoanProductsRegister
    {
         public  Task<bool> CreateLoanProductsAsync(LoanProductsRegister loanProductsRegister);
    }
}