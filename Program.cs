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

builder.Services.AddIdentityApiEndpoints<Employees>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<DBContext>();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//RequireAuthorization()
app.MapIdentityApi<Employees>();
app.MapPost("/Logout", async(SignInManager<Employees> signInManager, [FromBody] object empty) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
});

app.InventoryRoutes();
app.OrderRoutes();
app.CashierRoutes();
app.EmployeeRoutes();

//roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    foreach (var role in new[] {"Adm", "Manager", "Cashier"})
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole<Guid>(role));
    }
}

app.Run();
