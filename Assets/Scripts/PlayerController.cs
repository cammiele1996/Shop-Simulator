using UnityEngine;
using UnityEngine.InputSystem;  

public class PlayerController : MonoBehaviour
{
    public InputActionReference moveAction;

    public CharacterController charCon;

    public float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Vector 2 = X and Y axis
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

        // Use to print movement values to console
        //Debug.Log(moveInput);

        //transform.position = transform.position + new Vector3(moveInput.x * Time.deltaTime * moveSpeed, 0f, moveInput.y * Time.deltaTime * moveSpeed);

        Vector3 moveAmount = new Vector3(moveInput.x, 0f, moveInput.y);

        moveAmount = moveAmount * moveSpeed;

        charCon.Move(moveAmount * Time.deltaTime);

        
    }
}
