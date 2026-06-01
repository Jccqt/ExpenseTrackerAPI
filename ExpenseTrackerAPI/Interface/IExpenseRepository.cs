using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Interface
{
    public interface IExpenseRepository
    {
        Task<ServiceResponse<List<ExpenseDTO>>> GetAllExpenses();
        Task<ExpenseDTO> GetExpenseById(int expenseId);
        Task<ExpenseDTO> CreateExpense(ExpenseCreateDTO expense); 
    }
}
