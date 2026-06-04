using UnityEngine;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    public void Awake()
    {
        instance = this;
    }

    public GameObject updatePricePanel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenUpdatePrice()
    {
        updatePricePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseUpdatePrice()
    {
        updatePricePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }
}
