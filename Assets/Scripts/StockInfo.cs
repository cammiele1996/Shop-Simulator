using UnityEngine;

[System.Serializable] // Adds variables from script to editor manually (essentially)

public class StockInfo
{
    public string name;
    public float price;

    public StockObject stockObject;

    public enum StockType    // enum = unmodified list (Use for named constants)
    {                        // Here we are creating types of stock which will not be changed at any point, hence enum over list
                             
        cereal, bigDrink, tubeChips, fruit, fruitLarge
    }

    public StockType typeOfStock;   // Allows us to assign stock type in the editor
}
