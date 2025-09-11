using System.Linq;

namespace Loan_Management.Models
{
    public class LoanProductsRegister
    {
        //Core Identification
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

        //Financial terms
        public decimal PrincipalAmountMin { get; set; }
        public decimal PrincipalAmountMax { get; set; }
        public double InterestRate { get; set; }
        public string? InterestRateType { get; set; } //Fixed, Variable
        public decimal ProcessingFee { get; set; }
        public decimal LatePaymentPenalty { get; set; }

        //Repayment
        public string? RepaymentFrequency { get; set; }
        public int GracePeriodMonths { get; set; }
        public string? InstallmentType { get; set; }

        //Collateral
        public bool RequiresCollateral { get; set; }
        public string? CollateralType { get; set; }
        public string? EligibilityCriteria { get; set; }

        //Operational
        public bool Active { get; set; }
        public int MaxConcurrentLoans { get; set; }
        public bool PrepayMentAllowed { get; set; }
        public decimal? PrepayMentPenalty { get; set; }
        public string? Currency { get; set; }

        //Meta Data
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime LastUpdated { get; set; }


    }
}