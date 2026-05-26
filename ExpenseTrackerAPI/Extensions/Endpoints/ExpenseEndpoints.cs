using ExpenseTrackerAPI.Context;
using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Extensions.Endpoints
{
    public static class ExpenseEndpoints
    {
        public static void MapExpenseEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/expenses");

            group.MapGet("/", async (ExpenseDb db) =>
            await db.Expenses.ToListAsync());

            group.MapGet("/{id}", async (int id, ExpenseDb db) =>
                await db.Expenses.FindAsync(id)
                    is Expense expense ? Results.Ok(expense) : Results.NotFound());

            group.MapPost("/", async (Expense expense, ExpenseDb db) =>
            {
                db.Expenses.Add(expense);
                await db.SaveChangesAsync();
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
