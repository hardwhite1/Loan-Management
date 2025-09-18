using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Loan_Management.Models
{
    public class LoanProductsRegister
    {
        //Core Identification
        public Guid Id { get; set; }

        public required string UserId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }

        //Financial terms
        [Required]
        public decimal PrincipalAmountMin { get; set; }
        [Required]
        public decimal PrincipalAmountMax { get; set; }
        [Required]
        public double InterestRate { get; set; }
        [Required]
        public string? InterestRateType { get; set; } //Fixed, Variable
        [Required]
        public decimal ProcessingFee { get; set; }
        [Required]
        public decimal LatePaymentPenalty { get; set; }

        //Repayment
        [Required]
        public string? RepaymentFrequency { get; set; }
        [Required]
        public int GracePeriodMonths { get; set; }
        [Required]
        public string? InstallmentType { get; set; }

        //Collateral
        public bool RequiresCollateral { get; set; }
        public string? CollateralType { get; set; }
        public string? EligibilityCriteria { get; set; }

        //Operational
        [Required]
        public bool Active { get; set; }
        public int MaxConcurrentLoans { get; set; }
        public bool PrepayMentAllowed { get; set; }
        public decimal? PrepayMentPenalty { get; set; }
        public string? Currency { get; set; }

        //Meta Data
        [Required]
        public DateTimeOffset CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
    }
}