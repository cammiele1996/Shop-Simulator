using System.Collections.Generic;
using UnityEngine;

public class ShelfSpaceController : MonoBehaviour
{
    // Controls stock object information
    public StockInfo info;

    // List controlling objects on shelf
    public List<StockObject> objectsOnShelf;

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
            }
        }

        if (preventPlacing == false)
        {
            objectToPlace.transform.SetParent(transform);
            objectToPlace.MakePlaced();

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
