using TMPro;
using UnityEngine;

public class BuyStockFrameController : MonoBehaviour
{
    // Which stock object are we getting the information for?
    public StockInfo info;

    // TMP Text Reference for all text items
    public TMP_Text nameText, priceText, amountInBoxText, boxPriceText, buttonText;

    public StockBoxController boxToSpawn;

    private float boxCost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateFrameInfo();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFrameInfo()
    {
        info = StockInfoController.instance.GetInfo(info.name);

        nameText.text = info.name;
        priceText.text = "$" + info.price.ToString("F2");

        int boxAmount = boxToSpawn.GetStockAmount(info.typeOfStock);
        amountInBoxText.text = boxAmount.ToString() + " per box";

        boxCost = boxAmount * info.price;
        boxPriceText.text = "Box: $" + boxCost.ToString("F2");

        buttonText.text = "Pay: $" + boxCost.ToString("F2");

        
    }

    public void BuyBox()
    {
        if (StoreController.instance.CheckMoneyAvailable(boxCost) == true)
        {
            StoreController.instance.SpendMoney(boxCost);

            // Spawns box at the 
            Instantiate(boxToSpawn, StoreController.instance.stockSpawnPoint.position, Quaternion.identity).SetupBox(info);
        }
    }
}
