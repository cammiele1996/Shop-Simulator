using TMPro;
using UnityEngine;

public class LoginController : MonoBehaviour
{

    public static LoginController instance;

    private const string correctUsername = "Admin", 
                         correctPassword = "Password";

    public TMP_InputField username, password;

    private float sessionDuration = 20f, sessionTimer;

    public bool isLoggedIn;

    public BuyMenuController buyMenu;


    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buyMenu.ToggleButtonState(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoggedIn)
        {
            sessionTimer -= Time.deltaTime;
            Debug.Log("Session timer: " + sessionTimer);

            if (sessionTimer <= 0f)
            {
                Logout();
            }

        }
        
    }

    public void TryLogin()
    {
        Debug.Log("TryLogin called. Username: " + username.text + " Password: " + password.text);
        if (username.text == correctUsername && password.text == correctPassword)
        {
            isLoggedIn = true;
            sessionTimer = sessionDuration;
            Debug.Log("isLoggedIn: " + LoginController.instance.isLoggedIn);

            buyMenu.ToggleButtonState(true);
            buyMenu.OpenStockPanel();

            username.text = "";
            password.text = "";
        }
        else
        {
            password.text = "";
        }
    }

    public void Logout()
    {
        isLoggedIn = false;
        buyMenu.ToggleButtonState(false);
        buyMenu.OpenLoginPanel();

    }
}
