using System.Collections.ObjectModel;

namespace InventorySystem.Models;

public sealed class OrderBook
{
    private readonly Inventory _inventory;
    private readonly Stack<Order> _undoStack = new();

    public ObservableCollection<Order> QueuedOrders { get; } = new();
    public ObservableCollection<Order> ProcessedOrders { get; } = new();

    public OrderBook(Inventory inventory) => _inventory = inventory;

    public void QueueOrder(Order order) => QueuedOrders.Add(order);

    public Order? ProcessNextOrder()
    {
        if (QueuedOrders.Count == 0) return null;

        var next = QueuedOrders[0];
        QueuedOrders.RemoveAt(0);

        _inventory.Apply(next);
        ProcessedOrders.Add(next);

        _undoStack.Push(next);
        return next;
    }

    public bool CanUndo => _undoStack.Count > 0;

    public Order? UndoLastProcess()
    {
        if (!CanUndo) return null;

        var last = _undoStack.Pop();

        // Fjern 'last' fra ProcessedOrders (gå baglæns for at finde seneste forekomst)
        for (int i = ProcessedOrders.Count - 1; i >= 0; i--)
        {
            if (ReferenceEquals(ProcessedOrders[i], last))
            {
                ProcessedOrders.RemoveAt(i);
                break;
            }
        }

        // Rul lager tilbage
        _inventory.Revert(last);

        // Sæt ordren tilbage forrest i køen
        QueuedOrders.Insert(0, last);

        return last;
    }

    public decimal TotalRevenue() => ProcessedOrders.Sum(o => o.TotalPrice());
}