using TMPro;
using UnityEngine;

public class BuyFurnitureFrameController : MonoBehaviour
{

    public FurnitureController furniture;

    public TMP_Text priceText;


    public FurnitureController[] variants;

    public Animator[] buttonAnimators;

    private FurnitureController selectedVariant;

    private int selectedIndex = 0;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (variants != null && variants.Length > 0)
        {
            selectedVariant = variants[0];
            priceText.text = "Price: $" + furniture.price.ToString("F2");
            
        }
        else if (furniture != null)
        {
            priceText.text = "Price: $" + furniture.price.ToString("F2");
        }
        
    }

    void OnEnable()
    {
        if (buttonAnimators != null && buttonAnimators.Length > 0)
        {
            foreach (Animator anim in buttonAnimators)
            {
                anim.SetBool("isSelected", false);
            }

            buttonAnimators[selectedIndex].SetBool("isSelected", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyFurniture()
    {
        if (StoreController.instance.CheckMoneyAvailable(selectedVariant.price))
        {
            StoreController.instance.SpendMoney(selectedVariant.price);

            Instantiate(selectedVariant, StoreController.instance.furnitureSpawnPoint.position, Quaternion.identity);
        }
    }

    public void SelectVariant(int index)
    {
        selectedIndex = index;
        selectedVariant = variants[index];

        foreach (Animator anim in buttonAnimators)
        {
            anim.SetBool("isSelected", false);
          
        }

        buttonAnimators[index].SetBool("isSelected", true);
        
    }


}
