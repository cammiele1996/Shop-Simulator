using TMPro;
using UnityEngine;

public class BuyFurnitureFrameController : MonoBehaviour
{

    public FurnitureController furniture;

    public TMP_Text priceText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        priceText.text = "Price: $" + furniture.price.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyFurniture()
    {
        if (StoreController.instance.CheckMoneyAvailable(furniture.price))
        {
            StoreController.instance.SpendMoney(furniture.price);

            Instantiate(furniture, StoreController.instance.furnitureSpawnPoint.position, Quaternion.identity);
        }
    }
}
