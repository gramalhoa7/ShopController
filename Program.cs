using ShopControllerAPI.Models;
using ShopControllerAPI.Routes;
using ShopControllerAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<DBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.InventoryRoutes();
app.OrderRoutes();

app.UseHttpsRedirection();

app.Run();

