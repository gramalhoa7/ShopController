public class Receivables
{
    public Receivables(Guid? orderId, string description, decimal amount, DateTime dueDate, DateTime? receivedAt)
    {
        OrderId = orderId;
        Description = description;
        Amount = amount;
        DueDate = dueDate;
        ReceivedAt = receivedAt;
    }
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? OrderId { get; set; }            // de qual venda veio (pode ficar vazio)
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }         // quando deve cair
    public DateTime? ReceivedAt { get; set; }     // vazio = ainda não recebeu
}