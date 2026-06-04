using System.Collections.Generic;
using UnityEngine;

public class StockBoxController : MonoBehaviour
{
    // Reference to stock object type that is in the box
    public StockInfo info;

    // Creates a list for each of our product placement points, just like in Shelf Space Controller scipt
    public List<Transform> bigDrinkPoints, cerealPoints, tubeChipPoints, fruitPoints, largeFruitPoints;

    // Handles stock objects inside of the box
    public List<StockObject> stockInBox;

    public bool testFill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (testFill == true)
        {
            testFill = false;

            SetupBox(info);
        }
    }

    // Sets up the box with the information corresponding to the type of stock
    public void SetupBox(StockInfo stockType)
    {
        info = stockType;   // Assigns this info to the info corresponding with the type of stock in the box

        List<Transform> activePoints = new List<Transform>();   // Creates empty transform list to be filled with active points

        // Switch controlled by the variable Type Of Stock's info
        // Variable set in editor
        switch(info.typeOfStock)
        {
            // If the stock type is a big drink, set the activePoints to the big drink placement points
            // The same logic is the same for all stock in this switch statement
            case StockInfo.StockType.bigDrink:

                activePoints.AddRange(bigDrinkPoints);

                break;

            case StockInfo.StockType.cereal:

                activePoints.AddRange(cerealPoints);

                break;

            case StockInfo.StockType.tubeChips:

                activePoints.AddRange(tubeChipPoints);

                break;

            case StockInfo.StockType.fruit:

                activePoints.AddRange(fruitPoints);

                break;

            case StockInfo.StockType.fruitLarge:

                activePoints.AddRange(largeFruitPoints);

                break;
        }

        // If the box of stock is empty, execute the for loop
        if (stockInBox.Count == 0)
        {
            // Counts through the active points set above to place the objects and reset their positions
            // The loop creates a new stock object every time that runs for the amount of points that object can fit in the box
            for(int i = 0; i < activePoints.Count; i++)
            {
                // Instantiate clones and spawns a game object into your scene at runtime
                // Creates a stock object that is equal to the spawn of stock object at the active point [i]
                StockObject stock = Instantiate(stockType.stockObject, activePoints[i]);
                stock.transform.localPosition = Vector3.zero;           // Resets the local position of the stock object
                stock.transform.localRotation = Quaternion.identity;    // Resets the local rotation of the stock object

                // Adds the stock item to the box
                stockInBox.Add(stock);

                // Places the item in the box via the Place In Box function in Stock Object
                stock.PlaceInBox();
            }
        }
    }
}
