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
    }
}
