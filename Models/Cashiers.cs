namespace ShopControllerAPI.Models;

public class Cashiers
{
    public Cashiers(int nCashier, bool actived)
    {
        NCashier = nCashier;
        Actived = actived;

        Id = Guid.NewGuid();
    }
    public Guid Id { get; init; }
    public int NCashier { get; set; }
    public bool Actived { get; set; }
}