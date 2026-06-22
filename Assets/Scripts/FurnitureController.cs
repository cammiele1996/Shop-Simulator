using UnityEngine;

public class FurnitureController : MonoBehaviour
{

    public GameObject mainObject, placingObject;

    public Collider col;

    public float price;

    private Animator animator;

    private bool isOpen = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
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

    public void ToggleDoor()
    {
        if (animator != null)
        {

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.normalizedTime < 1f && !animator.IsInTransition(0))
            {
                return;
            }

            isOpen = !isOpen;
            animator.SetBool("isOpen", isOpen);
        }
    }
}
