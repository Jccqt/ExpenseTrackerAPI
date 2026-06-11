using ExpenseTrackerAPI.Context;
using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Interface;
using ExpenseTrackerAPI.Models;
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

        public async Task<ServiceResponse<List<ExpenseDTO>>> GetAllExpenses()
        {
            var response = new ServiceResponse<List<ExpenseDTO>>();
            var expenses = await _db.Expenses
                .AsNoTracking()
                .Select(e => new ExpenseDTO
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.Amount,
                    DateLogged = e.DateLogged
                }).ToListAsync();

            if(expenses.Count > 0)
            {
                response.Success = true;
                response.Message = "Expenses found.";
                response.Data = expenses;
            }
            else
            {
                response.Message = "No expenses found.";
            }

            return response;
        }

        public async Task<ServiceResponse<ExpenseDTO>> GetExpenseById(int expenseId)
        {
            var response = new ServiceResponse<ExpenseDTO>();
            var expense = await _db.Expenses
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == expenseId);

            if (expense == null)
            {
                response.Message = "Expense not found.";
                return response;
            }

            var result = new ExpenseDTO
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                DateLogged = expense.DateLogged
            };

            response.Success = true;
            response.Message = "Expense found.";
            response.Data = result;

            return response;
        }

        public async Task<ServiceResponse<ExpenseDTO>> CreateExpense(ExpenseCreateDTO expense)
        {
            var response = new ServiceResponse<ExpenseDTO>();

            var newExpense = new Expense
            {
                Description = expense.Description,
                Amount = expense.Amount,
                DateLogged = DateTime.UtcNow
            };

            _db.Expenses.Add(newExpense);
            await _db.SaveChangesAsync();

            var result = new ExpenseDTO
            {
                Id = newExpense.Id,
                Description = newExpense.Description,
                Amount = newExpense.Amount,
                DateLogged = newExpense.DateLogged
            };

            response.Success = true;
            response.Message = "Expense added successfully.";
            response.Data = result;

            return response;
        }

        public async Task<ServiceResponse<ExpenseDTO>> UpdateExpense(int expenseId, ExpenseUpdateDTO updatedExpense)
        {
            var response = new ServiceResponse<ExpenseDTO>();
            var expense = await _db.Expenses.FindAsync(expenseId);

            if (expense == null)
            {
                response.Message = "Expense not found.";
                return response;
            }

            if (updatedExpense.Description != null)
            {
                expense.Description = updatedExpense.Description;
            }

            if (updatedExpense.Amount != null)
            {
                expense.Amount = updatedExpense.Amount.Value;
            }

            await _db.SaveChangesAsync();

            var result = new ExpenseDTO
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                DateLogged = expense.DateLogged
            };

            response.Success = true;
            response.Message = "Expense updated successfully.";
            response.Data = result;

            return response;
        }

        public async Task<ServiceResponse> UpdateExpenseStatus(int expenseId, int status)
        {
            var response = new ServiceResponse();
            var expense = await _db.Expenses.FindAsync(expenseId);

            if (expense == null)
            {
                response.Message = "Expense not found.";
                return response;
            }

            expense.Status = status;

            await _db.SaveChangesAsync();

            response.Success = true;
            response.Message = "Expense status updated successfully.";

            return response;
        }

        public async Task<ServiceResponse> DeleteExpense(int expenseId)
        {
            var response = new ServiceResponse();
            var expense = await _db.Expenses.FindAsync(expenseId);

            if (expense == null)
            {
                response.Message = "Expense not found.";
                return response;
            }

            _db.Expenses.Remove(expense);
            await _db.SaveChangesAsync();

            response.Success = true;
            response.Message = "Expense deleted successfully.";

            return response;
        }
    }
}
