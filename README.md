# Shop Simulator
A first-person shop management simulator built in Unity 6 with C#.
This project is being developed as part of a Udemy Unity course and serves as the foundational learning ground for a larger original game — Merchant's Way: A Peasant's Tale.

---

## Current Version: v0.10.0

---

## About

**CURRENTLY IN ACTIVE DEVELOPMENT — LOGIC TESTING STATE**

The goal of this repo is to learn Unity's core systems through a hands-on store simulator project, including:

- Inventory and item management
- Economy and transaction systems
- First-person player controller and world interaction
- Furniture placement and interaction systems
- Day/night cycle and shop state management

---

## Features

### Gameplay
- First-person player controller with raycasting-based interaction
- Stock box pickup, opening, and shelf stocking system
- Furniture pickup and placement with transparent placement preview
- Recycling bin interaction for trash disposal
- Price update system per shelf

### Furniture
- Single Case, Double Case, Single Case XL, Double Case XL shelving units
- Large Fridge and Fridge XL with animated doors (open/close with interaction lock)
- Color variant selection system (Blue, Red, Green) for fridges
- All furniture purchasable via in-game shop menu

### UI
- Custom store management UI (Miele's All-In-One Store Manager)
- Stock and Furniture purchase panels with scrollable item frames
- Login system with session timer — re-authentication required after 20 minutes of inactivity
- Comma-formatted currency display
- Price update panel per shelf

### Stock
- Multiple stock types: Cereal, Big Drinks, Tube Chips, Fruit, Large Fruit
- Stock boxes spawn with correct items per type
- Shelf placement validation by stock type and capacity

---

## Tech Stack

- **Engine:** Unity 6 (URP)
- **Language:** C#
- **Assets:** Synty Studios Polygon Knights, Cute Supermarket Asset Pack
- **Version Control:** Git + Git LFS

---

## Related Project
The systems built here are being applied to an original game in development:

📜 **Merchant's Way: A Peasant's Tale** — A brutal, comedic first-person medieval shop simulator where you start as a penniless peasant and build a merchant empire from the ground up.
