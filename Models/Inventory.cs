namespace InventorySystem.Models;

public sealed class Inventory
{
    // Samme Item-instans skal bruges både i lageret og i ordrer (reference-lighed).
    private readonly Dictionary<Item, double> _stock = new();

    public void SetStock(Item item, double amount) => _stock[item] = amount;

    public double GetStock(Item item) => _stock.TryGetValue(item, out var a) ? a : 0.0;

    public IReadOnlyDictionary<Item, double> Stock => _stock;

    public IEnumerable<(Item item, double amount)> LowStockItems(double threshold = 5.0) =>
        _stock.Where(kv => kv.Value < threshold).Select(kv => (kv.Key, kv.Value));

    public void Apply(Order order)  // trækker lager ved proces
    {
        foreach (var line in order.OrderLines)
        {
            if (_stock.ContainsKey(line.Item))
            {
                _stock[line.Item] -= line.Quantity;
                if (_stock[line.Item] < 0) _stock[line.Item] = 0;
            }
        }
    }

    public void Revert(Order order) // fortryd: lægger på lager igen
    {
        foreach (var line in order.OrderLines)
        {
            if (_stock.ContainsKey(line.Item))
                _stock[line.Item] += line.Quantity;
            else
                _stock[line.Item] = line.Quantity;
        }
    }
}