using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    // Reference to UI controller (instance)
    public static UIController instance;

    // Reference to Price Panel
    public GameObject updatePricePanel;

    public GameObject buyMenuScreen;

    // Reference to base and current text prices
    public TMP_Text basePriceText, currentPriceText;

    // Reference to player input field within price updater
    public TMP_InputField priceInputField;

    public TMP_Text moneyText;

    // Reference to active stock object on shelf
    private StockInfo activeStockInfo;

    public void Awake()
    {
        instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            OpenCloseBuyMenu();
        }

        // Self added
        // If the price panel is open, the player can hit enter to apply the settings
        // If the player hits escape, it will close the menu
        if (updatePricePanel.activeSelf == true)
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                ApplyPriceUpdate();
            }

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                CloseUpdatePrice();
            }
        }

        
    }

    // Opens the price updater interface
    public void OpenUpdatePrice(StockInfo stockToUpdate)
    {
        updatePricePanel.SetActive(true);       // Opens panel

        Cursor.lockState = CursorLockMode.None; // Unlocks cursor

        basePriceText.text = "$" + stockToUpdate.price.ToString("F2");              // Updates base price the predefined price of the object on shelf
        currentPriceText.text = "$" + stockToUpdate.currentPrice.ToString("F2");    // Updates the current price to changable price within the game

        activeStockInfo = stockToUpdate;    // Updates the stock items info

        priceInputField.text = stockToUpdate.currentPrice.ToString("F2");          // Starts each open with a default text box
        priceInputField.textComponent.color = new Color(0f, 0f, 0f, 0.65f);
    }

    // Closes the price updater interface
    public void CloseUpdatePrice()
    {
        updatePricePanel.SetActive(false);          // Closes the panel

        Cursor.lockState = CursorLockMode.Locked;   // Locks cursor back to game
    }

    // Applies the price update for both the variable and the visuals on screen
    // This function is called by the in-editor "Apply Changes" button
    public void ApplyPriceUpdate()
    {

        // Set the current price to the number set in the input field by player
        activeStockInfo.currentPrice = float.Parse(priceInputField.text);

        // Updates the current price text
        currentPriceText.text = "$" + activeStockInfo.currentPrice.ToString("F2");

        // Updates the current price in the stock info controller script
        StockInfoController.instance.UpdatePrice(activeStockInfo.name, activeStockInfo.currentPrice);

        CloseUpdatePrice(); // Closes the menu
    }

    public void UpdateMoney(float currentMoney)
    {
        moneyText.text = "$" + currentMoney.ToString("F2");

    }

    public void OpenCloseBuyMenu()
    {
        if(buyMenuScreen.activeSelf == false)
        {
            buyMenuScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;

        }
        else
        {
            buyMenuScreen.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
