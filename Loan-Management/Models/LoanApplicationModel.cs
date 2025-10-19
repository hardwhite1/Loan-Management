using System.ComponentModel.DataAnnotations;

namespace Loan_Management.Models
{
    public class    LoanApplicationModel
    {
        //personal details
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string? Occupation { get; set; }
        public decimal NetIncome { get; set; }
        public string? PhoneNumber{ get; set; }
        public string? HomeTown { get; set; }
       
        //loan details
        public Guid Id { get; set; }

        public  LoanProductsRegister? LoanProduct { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public Guid LoanProductId { get; set; }
        [Required]
        [Range(1000, double.MaxValue, ErrorMessage = "Amount must be at least 1000")]
        public decimal RequestedAmount { get; set; }
        public bool RequiresCollateral { get; set; }
         [Required]
        public decimal ProcessingFee { get; set; }
        public string? CollateralDetails { get; set; }
        
        [Required]
        public int RepaymentPeriodMonths { get; set; }
        public string? Purpose { get; set; }

        //Workflow tracking
        [Required]
        public string? Status { get; set; } //Could be Pending, Under Review, Approved, Rejected, Disbursed
        public DateTimeOffset MaturityDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset ApplicationDate { get; set; } = DateTimeOffset.Now;
        //Audit
        public string? ProcessedBy { get; set; }
        
        
    }
}