using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Budget amount should be greater than 0.")]
        public int Amount { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}