using System.Collections.Generic;
using UnityEngine;

public class ShelfSpaceController : MonoBehaviour
{
    // Controls stock object information
    public StockInfo info;

    // List controlling objects on shelf
    public List<StockObject> objectsOnShelf;

    public List<Transform> bigDrinkPoints, cerealPoints, tubeChipPoints, fruitPoints, largeFruitPoints;

    // Handles stock placement
    public void PlaceStock(StockObject objectToPlace)
    {

        // Declares a temporary bool that controls whether or not we can place the item
        // Default is true
        bool preventPlacing = true;

        // If the amount of objects on the shelf is equal to 0, allow placement
        // Only executes if there are no items on the shelf
         if(objectsOnShelf.Count == 0)
        {
            
            info = objectToPlace.info;  // Sets the name of the object on the shelf to the name of the object
            preventPlacing = false;

        
        } else  // Exectues if there are items on the shelf
        {

            if(info.name == objectToPlace.info.name)
            {
                preventPlacing = false;

                switch(info.typeOfStock)
                {
                    case StockInfo.StockType.bigDrink:

                        if (objectsOnShelf.Count >= bigDrinkPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;

                    case StockInfo.StockType.cereal:

                        if (objectsOnShelf.Count >= cerealPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;

                    case StockInfo.StockType.tubeChips:

                        if (objectsOnShelf.Count >= tubeChipPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;

                    case StockInfo.StockType.fruit:

                        if (objectsOnShelf.Count >= fruitPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;

                    case StockInfo.StockType.fruitLarge:

                        if (objectsOnShelf.Count >= largeFruitPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;
                }

                

            }
        }

        if (preventPlacing == false)
        {
            
            objectToPlace.MakePlaced();

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
        }

    }

    public StockObject GetStock()
    {
        StockObject objectToReturn = null;

        if (objectsOnShelf.Count > 0)
        {
            objectToReturn = objectsOnShelf[objectsOnShelf.Count - 1];

            objectsOnShelf.RemoveAt(objectsOnShelf.Count - 1);
        }
        return objectToReturn;
    }
}
