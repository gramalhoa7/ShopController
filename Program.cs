using ShopControllerAPI.Routes;
using ShopControllerAPI.Data;
using Scalar.AspNetCore;
using ShopControllerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<DBContext>();

builder.Services.AddIdentityApiEndpoints<EmployeeAuth>()
    .AddEntityFrameworkStores<DBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//RequireAuthorization()
app.MapIdentityApi<EmployeeAuth>();
app.MapPost("/Logout", async(SignInManager<EmployeeAuth> signInManager, [FromBody] object empty) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
});

app.InventoryRoutes();
app.OrderRoutes();
app.CashierRoutes();
app.EmployeeRoutes();

app.UseHttpsRedirection();

app.Run();

