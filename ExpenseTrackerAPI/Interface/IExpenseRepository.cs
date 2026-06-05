using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Interface
{
    public interface IExpenseRepository
    {
        Task<ServiceResponse<List<ExpenseDTO>>> GetAllExpenses();
        Task<ServiceResponse<ExpenseDTO>> GetExpenseById(int expenseId);
        Task<ServiceResponse<ExpenseDTO>> CreateExpense(ExpenseCreateDTO expense);
        Task<ServiceResponse<ExpenseDTO>> UpdateExpense(int expenseId, ExpenseUpdateDTO updatedExpense);
        Task<ServiceResponse> DeleteExpense(int expenseId);
    }
}
