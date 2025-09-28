using System.ComponentModel.DataAnnotations;

namespace Loan_Management.Models
{
    public class LoanApplicationModel
    {
        //personal details
        public Guid Id { get; set; }

        public required LoanProductsRegister LoanProduct { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public Guid LoanProductId { get; set; }
        [Required]
        [Range(1000, double.MaxValue, ErrorMessage = "Amount must be at least 1000")]
        public decimal RequestedAmount { get; set; }
        public bool RequiresCollateral { get; set; }
        public string? CollateralDetails { get; set; }
        
        [Required]
        public int RepaymentPeriodMonths { get; set; }
        public string? Purpose { get; set; }

        //Workflow tracking
        [Required]
        public string? Status { get; set; } //Could be Pending, Under Review, Approved, Rejected, Disbursed
        public DateTimeOffset ApplicationDate { get; set; } = DateTimeOffset.Now;
        //Audit
        public string? ProcessedBy { get; set; }
        
        
    }
}