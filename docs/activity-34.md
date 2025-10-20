# Activity 34 – Visual relation between concepts

flowchart LR
  inv[inventory] -->|has many| item[item]
  item -->|has a| ppu[price per unit]
  bulk[bulk item] -->|is an| item
  inv -->|tracks / has| stock[stock (e.g. 3 pens)]
  cust[customer] -->|places| order[order]
  ob[order book] -->|queues / contains| order
