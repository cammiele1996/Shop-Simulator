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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the object is placed, reset the vector and quaternion back to (0,0,0)
        // Ensures the object is in it's default state while placed
        if (isPlaced == true)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, moveSpeed * Time.deltaTime);   // Resets object position to default
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, moveSpeed * Time.deltaTime);   // Resets object rotation to default
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

    }

    // Handles stock object placement
    public void MakePlaced()
    {
        rigBod.isKinematic = true;  // Sets objects rigid body's kinematics to true

        isPlaced = true;            // Sets objects placement to true


    }

    public void Release()
    {
        rigBod.isKinematic = false; // Removes object's rigid body's kinematics
    }
}
