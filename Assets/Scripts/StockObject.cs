using UnityEngine;

public class StockObject : MonoBehaviour
{
    public StockInfo info;

    // Controls the movement speed of a stock object
    public float moveSpeed;

    // Controls whether a stock object is considered placed
    public bool isPlaced;

    // Reference to our stock objects rigid body
    public Rigidbody rigBod;

    // Reference to our stock objects collider
    public Collider col;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        info = StockInfoController.instance.GetInfo(info.name);
    }

    // Update is called once per frame
    void Update()
    {
        // If the object is placed, reset the vector and quaternion back to (0,0,0)
        // Ensures the object is in it's default state while placed
        // Move Towards moves the object towards to location at a desired speed
        // Slerp is the same thing but for rotation
        if (isPlaced == true)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, moveSpeed * Time.deltaTime);   // Slowly moves object to position and resets local position
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, moveSpeed * Time.deltaTime);   // Slowly moves object to position and resets local rotation
        }
        
    }

    // Handles stock object pickup
    public void Pickup()   
    {
        rigBod.isKinematic = true; // Sets objects rigid body's kinematics to true

        transform.localPosition = Vector3.zero; // Sets location (x, y, z) to 0
        transform.localRotation = Quaternion.identity; // Sets rotation (x, y, z) to 0

        isPlaced = false;   // Ensures that our object is no longer considered placed, even if it wasn't before
                            // i.e. A object was thrown onto the ground and picked back up again

         col.enabled = false;

    }

    // Handles stock object placement
    public void MakePlaced()
    {
        rigBod.isKinematic = true;  // Sets objects rigid body's kinematics to true

        isPlaced = true;            // Sets objects placement to true

        col.enabled = false;


    }

    public void Release()
    {
        rigBod.isKinematic = false; // Removes object's rigid body's kinematics

        col.enabled = true;
    }

    // Sets the object's rigid body's kinematics to on
    // Removes the object's collider
    public void PlaceInBox()
    {
        rigBod.isKinematic = true;
        col.enabled = false;
    }
}
