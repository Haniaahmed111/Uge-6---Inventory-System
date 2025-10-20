```mermaid
flowchart LR
  %% Activity 34 - Visual relation between concepts (not UML)

  inv[Inventory]
  item[Item]
  ppu[Price per unit]
  bulk[Bulk item]
  stock[Stock eg 3 pens]
  cust[Customer]
  order[Order]
  ob[Order book]

  inv -->|has many| item
  item -->|has a| ppu
  bulk -->|is a| item
  inv -->|tracks / has| stock
  cust -->|places| order
  ob  -->|queues / contains| order

