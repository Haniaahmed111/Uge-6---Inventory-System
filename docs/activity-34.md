```mermaid
flowchart LR
  inv[Inventory] -->|has many| item[Item]
  bulk[Bulk item] -->|is an| item
  item -->|has a| ppu[Price per unit]
  inv -->|tracks / has| stock[Stock (e.g., 3 pens)]
  stock -. of .-> item
  cust[Customer] -->|places| order[Order]
  ob[Order book] -->|queues / contains| order


