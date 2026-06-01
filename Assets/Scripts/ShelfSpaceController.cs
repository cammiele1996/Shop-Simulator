using UnityEngine;

public class ShelfSpaceController : MonoBehaviour
{
    // Controls stock object information
    public StockInfo info;

    // Controls the amount of objects on a shelf
    public int amountOnShelf;

    public void PlaceStock(StockObject objectToPlace)
    {

        Debug.Log(objectToPlace.info.name);
        bool preventPlacing = true;

        if(amountOnShelf == 0)
        {

            info = objectToPlace.info;
            preventPlacing = false;

        
        } else
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

            amountOnShelf+= 1;    // Incriments amount of shelf
        }

    }
}
