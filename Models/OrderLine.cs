namespace InventorySystem.Models;

public sealed class OrderLine
{
    public Item Item { get; }
    // Antal for UnitItem, kg/m for BulkItem
    public double Quantity { get; }

    public OrderLine(Item item, double quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public decimal LineTotal => Item.PricePerUnit * (decimal)Quantity;

    public override string ToString() => $"{Item.Name} x {Quantity} = {LineTotal:C}";
}