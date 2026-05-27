using ExpenseTrackerAPI.DTOs;

namespace ExpenseTrackerAPI.Interface
{
    public interface IExpenseRepository
    {
        Task<List<ExpenseDTO>> GetAllExpenses();
        Task<ExpenseDTO> GetExpenseById(int expenseId);
    }
}
