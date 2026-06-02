using UnityEngine;

[System.Serializable] // Adds variables from script to editor manually (essentially)

public class StockInfo
{
    public string name;
    public float price;

    public enum StockType    // enum = list (from what I understand)
    {
        cereal, bigDrink, tubeChips, fruit, fruitLarge
    }

    public StockType typeOfStock;   // Allows us to assign stock type in the editor
}
