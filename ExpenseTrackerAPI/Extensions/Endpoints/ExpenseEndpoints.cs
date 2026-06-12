using ExpenseTrackerAPI.Context;
using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Interface;
using ExpenseTrackerAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Extensions.Endpoints
{
    public static class ExpenseEndpoints
    {
        public static void MapExpenseEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/expenses");

            group.MapGet("/", async (IExpenseRepository repo, CancellationToken ct) =>
            {
                var results = await repo.GetAllExpenses(ct);

                return results.Success ? Results.Ok(results) : Results.NotFound(results);
            });

            group.MapGet("/{expenseId}", async (int expenseId, IExpenseRepository repo, CancellationToken ct) =>
            {
                var result = await repo.GetExpenseById(expenseId, ct);
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

            group.MapPatch("/{expenseId}/status", async (int expenseId, [FromBody] int status, IExpenseRepository repo) =>
            {
                var result = await repo.UpdateExpenseStatus(expenseId, status);

                return result.Success ? Results.Ok(result) : Results.NotFound(result);
            });

            group.MapDelete("/{expenseId}", async (int expenseId, IExpenseRepository repo) =>
            {
                var result = await repo.DeleteExpense(expenseId);

                return result.Success ? Results.Ok(result) : Results.NotFound(result);
            });
        }
    }
}
