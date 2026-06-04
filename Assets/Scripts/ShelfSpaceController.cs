using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShelfSpaceController : MonoBehaviour
{
    // Controls stock object information
    public StockInfo info;

    // Lists controlling objects on shelf
    public List<StockObject> objectsOnShelf;
    public List<Transform> bigDrinkPoints, cerealPoints, tubeChipPoints, fruitPoints, largeFruitPoints;

    // Reference to UI Canvas TMP Text
    public TMP_Text shelfLabel;

    // Handles stock placement
    public void PlaceStock(StockObject objectToPlace)
    {

        // Declares a temporary bool that controls whether or not we can place the held stock object
        // Default is true
        bool preventPlacing = true;

        // If the amount of objects on the shelf is equal to 0, allow placement
        // Only executes if there are no items on the shelf
         if(objectsOnShelf.Count == 0)
        {
            
            info = objectToPlace.info;  // Sets the name of the object on the shelf to the name of the object being placed
            preventPlacing = false;     // Allows items to be placed on this shelf

        
        } else  // Exectues if there are items on the shelf
        {

            // If the name of the object to place is the same as the object on the shelf
            if(info.name == objectToPlace.info.name)
            {
                preventPlacing = false;

                // Switch to determine the type of stock that is on the shelf
                // Each IF statement says that if the shelf is full, then prevent placement
                switch(info.typeOfStock)
                {
                    case StockInfo.StockType.bigDrink:                      // Big Drink

                        if (objectsOnShelf.Count >= bigDrinkPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;

                    case StockInfo.StockType.cereal:                        // Cereal

                        if (objectsOnShelf.Count >= cerealPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;

                    case StockInfo.StockType.tubeChips:                     // Chips

                        if (objectsOnShelf.Count >= tubeChipPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;

                    case StockInfo.StockType.fruit:                         // Fruit
                           
                        if (objectsOnShelf.Count >= fruitPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;

                    case StockInfo.StockType.fruitLarge:                    // Large fruit

                        if (objectsOnShelf.Count >= largeFruitPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;
                }

                

            }
        }

         // If we are allowed to place the stock object, place the object
        if (preventPlacing == false)
        {
            
            objectToPlace.MakePlaced();     // Execute Make Placed function

            // Same switch logic as above, this time setting the parent to the next available point
            switch (info.typeOfStock)
            {
                case StockInfo.StockType.bigDrink:

                    objectToPlace.transform.SetParent(bigDrinkPoints[objectsOnShelf.Count]);

                    break;

                case StockInfo.StockType.cereal:

                    objectToPlace.transform.SetParent(cerealPoints[objectsOnShelf.Count]);

                    break;

                case StockInfo.StockType.tubeChips:

                    objectToPlace.transform.SetParent(tubeChipPoints[objectsOnShelf.Count]);

                    break;

                case StockInfo.StockType.fruit:

                    objectToPlace.transform.SetParent(fruitPoints[objectsOnShelf.Count]);

                    break;

                case StockInfo.StockType.fruitLarge:

                    objectToPlace.transform.SetParent(largeFruitPoints[objectsOnShelf.Count]);

                    break;
            }

            objectsOnShelf.Add(objectToPlace);

            UpdateDisplayPrice(info.currentPrice);

        }

    }

    // Handles removing stock from shelves
    public StockObject GetStock()
    {
        StockObject objectToReturn = null;      // Holds the stock object to be removed

        // This function is only ran IF the shelf has something on it
        if (objectsOnShelf.Count > 0)
        {
            objectToReturn = objectsOnShelf[objectsOnShelf.Count - 1];     // Removes the object internally

            objectsOnShelf.RemoveAt(objectsOnShelf.Count - 1);      // Removes the object in the same order it was put on
        }

        if(objectsOnShelf.Count == 0)
        {
            shelfLabel.text = string.Empty;
        }

        return objectToReturn;  // Returns the object to the player
    }

    public void StartPriceUpdate()
    {
        if (objectsOnShelf.Count > 0)
        {
            UIController.instance.OpenUpdatePrice(info);
        }
    }

    public void UpdateDisplayPrice(float price)
    {
        if (objectsOnShelf.Count > 0)
        {
            info.currentPrice = price;

            shelfLabel.text = "$" + info.currentPrice.ToString("F2");
        }
        
    }
}
