using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<Budget>? Budgets { get; set; } // Added budgets
    }
}