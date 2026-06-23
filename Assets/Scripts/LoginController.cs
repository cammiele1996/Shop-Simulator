using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoginController : MonoBehaviour
{

    public static LoginController instance;

    private const string correctUsername = "Admin", 
                         correctPassword = "Password";

    public TMP_InputField username, password;
    public TMP_Text sessionExpired, incorrectCredentials;

    private float sessionDuration = 1200f, sessionTimer;

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

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (username.isFocused)
            {
                password.Select();
            }
            else if (password.isFocused)
            {
                username.Select();
            }
           
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            TryLogin();
        }


        if (isLoggedIn)
        {
            sessionTimer -= Time.deltaTime;

            if (sessionTimer <= 0f)
            {
                Logout();
            }

        }
        
    }

    public void TryLogin()
    {
        
        if (username.text == correctUsername && password.text == correctPassword)
        {
            isLoggedIn = true;
            sessionTimer = sessionDuration;
            

            buyMenu.ToggleButtonState(true);
            buyMenu.OpenStockPanel();

            username.text = "";
            password.text = "";
        }
        else
        {
            sessionExpired.gameObject.SetActive(false);
            incorrectCredentials.gameObject.SetActive(true);
            password.text = "";
        }
    }

    public void Logout()
    {
        isLoggedIn = false;
        buyMenu.ToggleButtonState(false);
        buyMenu.OpenLoginPanel();

        sessionExpired.gameObject.SetActive(true);
        incorrectCredentials.gameObject.SetActive(false);

    }
}
