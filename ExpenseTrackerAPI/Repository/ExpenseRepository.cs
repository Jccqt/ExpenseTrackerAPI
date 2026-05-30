using ExpenseTrackerAPI.Context;
using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseDb _db;

        public ExpenseRepository(ExpenseDb db)
        {
            _db = db;
        }

        public async Task<List<ExpenseDTO>> GetAllExpenses()
        {
            return await _db.Expenses
                .Select(e => new ExpenseDTO
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.Amount,
                    DateLogged = e.DateLogged
                }).ToListAsync();
        }

        public async Task<ExpenseDTO> GetExpenseById(int expenseId)
        {
            var expense = await _db.Expenses.FindAsync(expenseId);

            if (expense == null) return null;

            return new ExpenseDTO
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                DateLogged = expense.DateLogged
            };
        }
    }
}
