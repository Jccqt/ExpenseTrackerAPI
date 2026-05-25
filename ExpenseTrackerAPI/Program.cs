using ExpenseTrackerAPI.Context;
using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ExpenseDb>(opt => opt.UseInMemoryDatabase("ExpenseList"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoints

// GET: Retieve all expenses
app.MapGet("/expenses", async (ExpenseDb db) =>
    await db.Expenses.ToListAsync());

// GET: Retrieve a specific expense by ID
app.MapGet("/expenses/{expenseId}", async (int expenseId, ExpenseDb db) =>
    await db.Expenses.FindAsync(expenseId)
    is Expense expense
    ? Results.Ok(expense)
    : Results.NotFound());

app.Run();

