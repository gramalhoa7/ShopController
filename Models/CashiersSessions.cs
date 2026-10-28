using System.Data;

namespace ShopControllerAPI.Models;

public class CashiersSessions
{
    public CashiersSessions(Guid cashierId, Guid employeeId, decimal openingAmount)
    {
        CashierId = cashierId;
        EmployeeId = employeeId;
        OpeningAmount = openingAmount;
        Opened = DateTime.UtcNow;
        Id = Guid.NewGuid();
    }
    public Guid Id { get; init; }

    public Guid CashierId { get; set; }
    public Cashiers Cashier { get; set; } = null!;

    public Guid EmployeeId { get; set; }
    public Employees Employee { get; set; } = null!;

    public DateTime Opened { get; set; }
    public DateTime? Closed { get; set; }
    public decimal OpeningAmount { get; set; }
    public decimal ClosingAmount { get; set; }
}