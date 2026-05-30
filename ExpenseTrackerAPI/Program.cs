using ExpenseTrackerAPI.Context;
using ExpenseTrackerAPI.Extensions.Endpoints;
using ExpenseTrackerAPI.Interface;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ExpenseDb>(opt => opt.UseInMemoryDatabase("ExpenseList"));

builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoint Extension
app.MapExpenseEndpoints();

app.Run();

