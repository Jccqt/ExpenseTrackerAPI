using ExpenseTrackerAPI.Context;
using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Interface;
using ExpenseTrackerAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Extensions.Endpoints
{
    public static class ExpenseEndpoints
    {
        public static void MapExpenseEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/expenses");

            group.MapGet("/", async (IExpenseRepository repo) =>
            {
                var results = await repo.GetAllExpenses();

                return results.Success ? Results.Ok(results) : Results.NotFound(results);
            });

            group.MapGet("/{expenseId}", async (int expenseId, IExpenseRepository repo) =>
            {
                var expense = await repo.GetExpenseById(expenseId);
                return expense is not null ? Results.Ok(expense) : Results.NotFound(expense);
            });
                
            group.MapPost("/", async (ExpenseCreateDTO dto, IExpenseRepository repo) =>
            {
                var expense = await repo.CreateExpense(dto);
                return Results.Created($"/expenses/{expense.Id}", expense);
            });

            group.MapDelete("/{expenseId}", async (int expenseId, ExpenseDb db) =>
            {
                if (await db.Expenses.FindAsync(expenseId) is Expense expense)
                {
                    db.Expenses.Remove(expense);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                return Results.NotFound();
            });
        }
    }
}
