using ShopControllerAPI.Routes;
using ShopControllerAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ShopControllerAPI;
using System.Text;
using Scalar.AspNetCore;
using ShopControllerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DBContext>();
var key = Encoding.ASCII.GetBytes(Key.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<EmployeeAuth>()
    .AddEntityFrameworkStores<DBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseSwagger();
app.UseSwaggerUI();
app.MapSwagger();//RequireAuthorization()
app.MapIdentityApi<EmployeeAuth>();
app.MapPost("/Logout", async(SignInManager<EmployeeAuth> signInManager, [FromBody] object empty) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
});

app.InventoryRoutes();
app.OrderRoutes();
app.CashierRoutes();

app.UseHttpsRedirection();

app.Run();

