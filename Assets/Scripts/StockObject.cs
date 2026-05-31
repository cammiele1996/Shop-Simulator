using UnityEngine;

public class StockObject : MonoBehaviour
{
    public float moveSpeed;

    private bool isPlaced;

    public Rigidbody rigBod;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

        isPlaced = false;

    }

    // Handles stock object placement
    public void MakePlaced()
    {
        rigBod.isKinematic = true;

        isPlaced = true;


    }

    public void Release()
    {
        rigBod.isKinematic = false;
    }
}
