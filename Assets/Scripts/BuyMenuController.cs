using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenuController : MonoBehaviour
{
    public GameObject stockPanel, furniturePanel, loginPanel;

    public Button stockButton, furnitureButton, logOutButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleButtonState(bool state)
    {
        stockButton.gameObject.SetActive(state);
        furnitureButton.gameObject.SetActive(state);
        logOutButton.gameObject.SetActive(state);
    }

    public void OpenStockPanel()
    {
        stockPanel.SetActive(true);
        furniturePanel.SetActive(false);
        loginPanel.SetActive(false);
    }

    public void OpenFurniturePanel()
    {
        stockPanel.SetActive(false);
        furniturePanel.SetActive(true);
        loginPanel.SetActive(false);
    }

    public void OpenLoginPanel()
    {
        stockPanel.SetActive(false);
        furniturePanel.SetActive(false);
        loginPanel.SetActive(true);
    }
}
