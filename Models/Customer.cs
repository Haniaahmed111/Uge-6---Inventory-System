namespace InventorySystem.Models;

public sealed class Customer
{
    public string Name { get; }
    public List<Order> Orders { get; } = new();

    public Customer(string name) => Name = name;

    public void CreateOrder(Order order)
    {
        Orders.Add(order);
    }

    public override string ToString() => Name;
}