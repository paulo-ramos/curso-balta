namespace Store.Domain.Entities;
public class Product : Entity
{
    public Product(string ttile, decimal price, bool active)
    {
        Ttile = ttile;
        Price = price;
        Active = active;
    }

    public string Ttile { get; private set; }
    public decimal Price { get; private set; }
    public bool Active { get; private set; }

}
