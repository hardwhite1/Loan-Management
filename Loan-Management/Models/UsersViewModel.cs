
namespace Loan_Management.Models
{
    public class UsersViewModel
    {
        public ApplicationUser[]? Adminstrators { get; set; }

        public ApplicationUser[]? Everyone { get; set; }
    }
}