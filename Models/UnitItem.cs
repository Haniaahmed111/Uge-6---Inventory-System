namespace InventorySystem.Models;

public sealed class UnitItem : Item
{
    // Vægt pr. stk. er ikke strengt nødvendig for logikken,
    // men er et felt i opgavebeskrivelsen:
    public double WeightPerItemKg { get; }

    public UnitItem(string name, decimal pricePerUnit, double weightPerItemKg)
        : base(name, pricePerUnit)
    {
        WeightPerItemKg = weightPerItemKg;
    }

    public override string ToString() => $"{Name} ({PricePerUnit:C} / pcs)";
}