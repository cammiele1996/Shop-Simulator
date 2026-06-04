using System.Collections.Generic;
using UnityEngine;

public class StockBoxController : MonoBehaviour
{
    // Self Added
    public Vector3 flap1OpenRotation, flap2OpenRotation;
    private Vector3 flap1ClosedRotation, flap2ClosedRotation;

    // Reference to stock object type that is in the box
    public StockInfo info;

    // Creates a list for each of our product placement points, just like in Shelf Space Controller scipt
    public List<Transform> bigDrinkPoints, cerealPoints, tubeChipPoints, fruitPoints, largeFruitPoints;

    // Handles stock objects inside of the box
    public List<StockObject> stockInBox;

    // Creates our test checkbox to fill a box in Unity
    public bool testFill;

    // Reference to stock box's rigid body
    public Rigidbody rigBod;

    // Reference to stock box's collider
    public Collider col;

    public float moveSpeed = 5f; // Default value of 5

    public GameObject flap1, flap2;

    public bool isOpen = false;


    private bool isHeld;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flap1ClosedRotation = flap1.transform.localEulerAngles;
        flap2ClosedRotation = flap2.transform.localEulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        // If the checkbox in Unity is checked, the test will fun
        if (testFill == true)
        {
            testFill = false;

            // Sets up a stock box with default info
            SetupBox(info);
        }

        if(isHeld == true)
        {
            // See lines 29-33. Same code and has explaination if needed
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, moveSpeed * Time.deltaTime);   
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, moveSpeed * Time.deltaTime); 
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

    // Sets the object's rigid body's kinematics to on
    // Removes the object's collider
    public void PlaceInBox()
    {
        rigBod.isKinematic = true;
        col.enabled = false;
    }

    public void Pickup()
    {
        rigBod.isKinematic = true; // Sets objects rigid body's kinematics to true

        col.enabled = false;

        isHeld = true;

    }

    public void Release()
    {
        rigBod.isKinematic = false; // Removes object's rigid body's kinematics

        col.enabled = true;

        isHeld = false;
    }

    public void OpenClose()
    {
        if(isOpen)
        {
            flap1.transform.localEulerAngles = flap1ClosedRotation;
            flap2.transform.localEulerAngles= flap2ClosedRotation;

        }

        else
        {
            flap1.transform.localEulerAngles = flap1OpenRotation;
            flap2.transform.localEulerAngles = flap2OpenRotation;

        }

        isOpen = !isOpen;
    }

    public void PlaceStockOnShelf(ShelfSpaceController shelf)
    {
        if(stockInBox.Count > 0)
        {
            shelf.PlaceStock(stockInBox[stockInBox.Count - 1]);

            if (stockInBox[stockInBox.Count - 1].isPlaced == true)
            {
                stockInBox.RemoveAt(stockInBox.Count - 1);
            }
        }
    }
}
