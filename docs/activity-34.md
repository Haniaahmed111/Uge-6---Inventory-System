# Activity 34 – Visual relation between concepts

```mermaid

flowchart LR
  %% Activity 34 – Visual relation between concepts (ikke UML-klasser)
  inv[inventory]
  item[item]
  ppu[price per unit]
  bulk[bulk item]
  stock[stock (e.g. 3 pens)]
  cust[customer]
  order[order]
  ob[order book]

  inv -->|has many| item
  item -->|has a| ppu
  bulk -->|is an| item
  inv -->|tracks / has| stock
  cust -->|places| order
  ob -->|queues / contains| order
