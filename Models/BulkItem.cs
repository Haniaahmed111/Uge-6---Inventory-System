namespace InventorySystem.Models;

public sealed class BulkItem : Item
{
    // Fx "kg", "m"
    public string MeasurementUnit { get; }

    public BulkItem(string name, decimal pricePerUnit, string measurementUnit)
        : base(name, pricePerUnit)
    {
        MeasurementUnit = measurementUnit;
    }

    public override string ToString() => $"{Name} ({PricePerUnit:C} / {MeasurementUnit})";
}