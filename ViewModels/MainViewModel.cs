using System.Collections.ObjectModel;
using InventorySystem.Models;

namespace InventorySystem.ViewModels;

public sealed class MainViewModel : BindableBase
{
    public Inventory Inventory { get; }
    public OrderBook OrderBook { get; }

    public ObservableCollection<Order> QueuedOrders => OrderBook.QueuedOrders;
    public ObservableCollection<Order> ProcessedOrders => OrderBook.ProcessedOrders;

    public DelegateCommand ProcessNextOrderCommand { get; }
    public DelegateCommand UndoLastProcessCommand { get; }

    public decimal TotalRevenue => OrderBook.TotalRevenue();

    // Low stock tekstliste (som før)
    public IEnumerable<string> LowStockDisplay =>
        Inventory.LowStockItems(5).Select(t => $"{t.item.Name}: {t.amount}");

    // Stock-rows til DataGrid
    public IEnumerable<StockRow> StockRows =>
        Inventory.Stock.Select(kv => new StockRow(
            kv.Key.Name,
            kv.Value,
            kv.Key is BulkItem bi ? bi.MeasurementUnit : "pcs",
            kv.Key.PricePerUnit
        ));

    public MainViewModel()
    {
        // 1) Opret varer
        var pen   = new UnitItem("Pen",   pricePerUnit: 10m,  weightPerItemKg: 0.02);
        var soap  = new UnitItem("Soap",  pricePerUnit: 15m,  weightPerItemKg: 0.10);
        var rice  = new BulkItem("Rice",  pricePerUnit: 20m,  measurementUnit: "kg");
        var sugar = new BulkItem("Sugar", pricePerUnit: 12m,  measurementUnit: "kg");

        // 2) Lager
        Inventory = new Inventory();
        Inventory.SetStock(pen,  10);
        Inventory.SetStock(soap, 8);
        Inventory.SetStock(rice, 50);
        Inventory.SetStock(sugar, 6);

        // 3) Ordrebog
        OrderBook = new OrderBook(Inventory);

        var alice = new Customer("Alice");
        var bob   = new Customer("Bob");

        var o1 = new Order(alice, new[]
        {
            new OrderLine(pen, 2),
            new OrderLine(rice, 3.5)
        });
        alice.CreateOrder(o1);

        var o2 = new Order(bob, new[]
        {
            new OrderLine(soap, 1),
            new OrderLine(sugar, 2.0)
        });
        bob.CreateOrder(o2);

        var o3 = new Order(alice, new[]
        {
            new OrderLine(pen, 1),
            new OrderLine(soap, 2)
        });
        alice.CreateOrder(o3);

        OrderBook.QueueOrder(o1);
        OrderBook.QueueOrder(o2);
        OrderBook.QueueOrder(o3);

        // 4) Kommandoer
        ProcessNextOrderCommand = new DelegateCommand(
            ProcessNextOrder,
            () => QueuedOrders.Count > 0
        );

        UndoLastProcessCommand = new DelegateCommand(
            UndoLastProcess,
            () => OrderBook.CanUndo
        );

        // 5) Reager på ændringer
        OrderBook.QueuedOrders.CollectionChanged += (_, __) =>
        {
            ProcessNextOrderCommand.RaiseCanExecuteChanged();
        };

        OrderBook.ProcessedOrders.CollectionChanged += (_, __) =>
        {
            Raise(nameof(TotalRevenue));
            Raise(nameof(LowStockDisplay));
            Raise(nameof(StockRows));
            UndoLastProcessCommand.RaiseCanExecuteChanged();
        };

        // 6) Init UI
        Raise(nameof(TotalRevenue));
        Raise(nameof(LowStockDisplay));
        Raise(nameof(StockRows));
        ProcessNextOrderCommand.RaiseCanExecuteChanged();
        UndoLastProcessCommand.RaiseCanExecuteChanged();
    }

    private void ProcessNextOrder()
    {
        var _ = OrderBook.ProcessNextOrder();
        Raise(nameof(TotalRevenue));
        Raise(nameof(LowStockDisplay));
        Raise(nameof(StockRows));
        ProcessNextOrderCommand.RaiseCanExecuteChanged();
        UndoLastProcessCommand.RaiseCanExecuteChanged();
    }

    private void UndoLastProcess()
    {
        var _ = OrderBook.UndoLastProcess();
        Raise(nameof(TotalRevenue));
        Raise(nameof(LowStockDisplay));
        Raise(nameof(StockRows));
        ProcessNextOrderCommand.RaiseCanExecuteChanged();
        UndoLastProcessCommand.RaiseCanExecuteChanged();
    }

    // Row-type til lager-DataGrid
    public sealed record StockRow(string Item, double Amount, string Unit, decimal PricePerUnit)
    {
        public string AmountWithUnit => $"{Amount} {Unit}";
        public string PriceDisplay => $"{PricePerUnit:C}";
    }
}

