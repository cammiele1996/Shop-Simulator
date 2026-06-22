using UnityEngine;

public class FurnitureController : MonoBehaviour
{

    public GameObject mainObject, placingObject;

    public Collider col;

    public float price;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakePlaceable()
    {
        mainObject.SetActive(false);
        placingObject.SetActive(true);
        col.enabled = false;
    }

    public void PlaceFurniture()
    {
        mainObject.SetActive(true);
        placingObject.SetActive(false);
        col.enabled = true;
    }
}
