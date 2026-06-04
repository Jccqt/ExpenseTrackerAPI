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
                var result = await repo.GetExpenseById(expenseId);
                return result.Success ? Results.Ok(result) : Results.NotFound(result);
            });
                
            group.MapPost("/", async (ExpenseCreateDTO dto, IExpenseRepository repo) =>
            {
                var result = await repo.CreateExpense(dto);
                return Results.Created($"/expenses/{result.Data.Id}", result);
            });

            group.MapPatch("/{expenseId}", async (int expenseId, ExpenseUpdateDTO dto, IExpenseRepository repo) =>
            {
                var result = await repo.UpdateExpense(expenseId, dto);

                return result.Success ? Results.Ok(result) : Results.NotFound(result);
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
