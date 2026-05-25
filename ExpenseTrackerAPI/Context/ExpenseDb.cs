using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Context
{
    public class ExpenseDb : DbContext
    {
        public ExpenseDb(DbContextOptions<ExpenseDb> options) : base(options) { }
        public DbSet<Expense> Expenses => Set<Expense>();
    }
}
