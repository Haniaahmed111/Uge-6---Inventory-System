namespace InventorySystem.Models;

public sealed class Order
{
    public DateTime Time { get; }
    public Customer Customer { get; }
    public List<OrderLine> OrderLines { get; }

    public Order(Customer customer, IEnumerable<OrderLine> lines)
    {
        Customer = customer;
        OrderLines = lines.ToList();
        Time = DateTime.Now;
    }

    public decimal TotalPrice() => OrderLines.Sum(l => l.LineTotal);

    // Hjælpefelter til DataGrid:
    public string CustomerName => Customer?.Name ?? "";
    public int LinesCount => OrderLines.Count;
    public decimal Total => TotalPrice();
}