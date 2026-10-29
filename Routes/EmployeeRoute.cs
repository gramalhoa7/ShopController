using Microsoft.EntityFrameworkCore;
using ShopControllerAPI.Data;
using ShopControllerAPI.Models;

namespace ShopControllerAPI.Routes;

public static class EmployeeRoute
{
    public static void EmployeeRoutes(this WebApplication app)
    {
        app.MapPost("Employee", async (EmployeesRequest req, DBContext context) =>
        {
            var employee = new Employees(req.Name, req.Age, req.Department, req.Employed);

            await context.AddAsync(employee);
            await context.SaveChangesAsync();
        });

        app.MapGet("Employee", async (DBContext context) =>
        {
            var employee = await context.Employee.ToListAsync();
            return Results.Ok(employee);
        });

        app.MapPut("Employee/{Id:guid}", async (Guid id, EmployeesRequest req, DBContext context) =>
        {
            var employee = await context.Employee.FirstOrDefaultAsync(o => o.Id == id);

            if (employee == null) return Results.NotFound();

            employee.Name = req.Name;
            employee.Age = req.Age;
            employee.Department = req.Department;

            await context.SaveChangesAsync();

            return Results.Ok(employee);
        });

        app.MapDelete("Employee/{Id:guid}", async (Guid id, DBContext context) =>
        {
            var employee = await context.Employee.FirstOrDefaultAsync(o => o.Id == id);

            if (employee == null) return Results.NotFound();

            employee.Employed = false;

            await context.SaveChangesAsync();
            
            return Results.Ok(employee);
        });
    }
}