using UnityEngine;

[System.Serializable] // Adds variables from script to editor manually (essentially)

public class StockInfo
{
    public string name;

   public enum StockType
    {
        cereal, bigDrink, chipsTube, fruit, fruitLarge
    }

    public StockType typeOfStock;
}
