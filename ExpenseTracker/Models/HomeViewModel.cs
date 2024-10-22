using System.Collections.Generic;

namespace ExpenseTracker.Models
{
    public class HomeViewModel
    {
        public int Income { get; set; }
        public int Expenses { get; set; }
        public int Balance { get; set; }

        public List<Transaction> RecentTransactions { get; set; }
        public List<Budget> Budgets { get; set; }
    }
}