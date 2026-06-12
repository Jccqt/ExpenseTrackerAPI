using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Interface
{
    public interface IExpenseRepository
    {
        Task<ServiceResponse<List<ExpenseDTO>>> GetAllExpenses(CancellationToken ct = default);
        Task<ServiceResponse<ExpenseDTO>> GetExpenseById(int expenseId, CancellationToken ct = default);
        Task<ServiceResponse<ExpenseDTO>> CreateExpense(ExpenseCreateDTO expense);
        Task<ServiceResponse<ExpenseDTO>> UpdateExpense(int expenseId, ExpenseUpdateDTO updatedExpense);
        Task<ServiceResponse> UpdateExpenseStatus(int expenseId, int status);
        Task<ServiceResponse> DeleteExpense(int expenseId);
    }
}
