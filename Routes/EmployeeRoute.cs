using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ShopControllerAPI.Data;
using ShopControllerAPI.Models;

namespace ShopControllerAPI.Routes;

public static class EmployeeRoute
{
    public static void EmployeeRoutes(this WebApplication app)
    {       
        app.MapGet("Employee", async (DBContext context) =>
        {
            var employee = await context.Users.ToListAsync();
            return Results.Ok(employee);
        }).RequireAuthorization(o => o.RequireRole("Adm", "Manager"));

        app.MapPut("Employee/{Id:guid}", async (Guid id, EmployeesRequest req, DBContext context) =>
        {
            var employee = await context.Users.FirstOrDefaultAsync(o => o.Id == id);

            if (employee == null) return Results.NotFound();
            if (req.Name == null || req.Age <= 0 || req.Department == null) return Results.BadRequest();

            employee.Name = req.Name;
            employee.Age = req.Age;
            employee.Department = req.Department;

            await context.SaveChangesAsync();

            return Results.Ok(employee);
        }).RequireAuthorization(o => o.RequireRole("Adm"));

        app.MapDelete("Employee/{Id:guid}", async (Guid id, DBContext context) =>
        {
            var employee = await context.Users.FirstOrDefaultAsync(o => o.Id == id);

            if (employee == null) return Results.NotFound();

            employee.Employed = false;

            await context.SaveChangesAsync();
            
            return Results.Ok(employee);
        }).RequireAuthorization(o => o.RequireRole("Adm"));
    }
}