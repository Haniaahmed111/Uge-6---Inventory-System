namespace InventorySystem.Models;

public abstract class Item
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }
    public decimal PricePerUnit { get; }

    protected Item(string name, decimal pricePerUnit)
    {
        Name = name;
        PricePerUnit = pricePerUnit;
    }

    public override string ToString() => $"{Name} ({PricePerUnit:C} / unit)";
}