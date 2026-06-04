using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem; // Automatically added from InputActionReference

public class PlayerController : MonoBehaviour
{
    //                      ***Public Variables***

    // Public variables create an interactable toggle in Unity



    //              ---Input Action References---

    // References Unity input system
    // Controls player movement
    public InputActionReference moveAction;

    // Controls player jump
    public InputActionReference jumpAction;
    
    // Controls player look
    public InputActionReference lookAction;

    // References Character Controller system
    // Controls Unity's CharacterController
    public CharacterController charCon;



    //                    ---Floats---

    // Controls player movement speed
    public float moveSpeed;

    // Controls player jump force
    public float jumpForce;

    // Controls player look speed
    public float lookSpeed;

    // Controls look limit
    public float minLookAngle, maxLookAngle;

    // Controls the range a player can interact with an object in the game world
    public float interactionRange;

    // Controls our player throw force
    public float throwForce;

    public float crouchHeight;

    public float standHeight;

    public float crouchSpeed;

    private bool isCrouching = false;



    //                  ---Layer Masks---

    // LayerMask references Layer system in Unity
    // Creates a dropdown in Unity to assign a layer to this variable
    public LayerMask whatIsStock;

    public LayerMask whatIsShelf;

    public LayerMask whatIsStockBox;

    //                   ---Transforms---

    // References our hold point empty
    public Transform holdPoint;

    public Transform boxHoldPoint;

    //               ---Camera References---

    // Reference to player camera
    public Camera theCam;

    //               ---Script References---

    // Controls the box held by the player
    public StockBoxController heldBox;





    //                        ***Private Variables***
    // Private variables don't show up in Unity Editor

    // Controls player 
    private float ySpeed;

    // Controls horizontal and vertical rotation
    private float horiRot, vertRot;

    // Controls pickup held by player
    private StockObject heldPickup;



    //                          ***Main Script***

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Locks cursor to game window and hides it
        // None allows the mouse to move freely
        // Confined locks the cursor to the window but is visable
        // Locked locks cursor to the center of the screen and hides it
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // If an instance of the Update Price Panel exists, check to see if that instance is active
        if(UIController.instance.updatePricePanel != null)
        {
            // If that instance is active, restart update loop
            // If the Update screen is open, the rest of the player controller is voided until closed
            if (UIController.instance.updatePricePanel.activeSelf == true)
            {
                return;
            }
        }

        //                          ---Look---

        // Vector 2 = X and Y axis
        // Get look input
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        // Sets horizontal rotation to the its current position plus the input
        // Then it is multiplied by 1/x where x equals the current framerate
        // Using Time.deltaTime prevents framerate dependencies
        horiRot += lookInput.x * Time.deltaTime * lookSpeed;
        transform.rotation = Quaternion.Euler(0f, horiRot, 0f); // Handles horizontal look input

        // Same logic handles vertical rotation
        vertRot -= lookInput.y * Time.deltaTime * lookSpeed;

        // Clamp sets a range for the first value between the second and third values
        // In this case, our vertical rotation is locked to the values set in Unity
        vertRot = Mathf.Clamp(vertRot, minLookAngle, maxLookAngle);
        theCam.transform.localRotation = Quaternion.Euler(vertRot, 0f, 0f);





        //                       ---Movement---

        // Gets move input (WASD & Spacebar)
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

        // Vector 3 = X, Y, and Z axis
        // Aligns movement with camera
        Vector3 vertMove = transform.forward * moveInput.y;
        Vector3 horiMove = transform.right * moveInput.x;
        Vector3 moveAmount = horiMove + vertMove;
        moveAmount = moveAmount.normalized;

        // Sets amount of movement to the move speed set in Unity
        moveAmount = moveAmount * moveSpeed;

        // Checks character controller in Unity to see if object is on the ground
        // Are we on the ground?
        if (charCon.isGrounded == true)
        {
            ySpeed = 0f;

            // Handles jump logic
            // Are we jumping?
            if (jumpAction.action.WasPressedThisFrame())
            {
                ySpeed = jumpForce;
            }
        }

        // Gives player gravity
        ySpeed = ySpeed + (Physics.gravity.y * Time.deltaTime);
        moveAmount.y = ySpeed;

        // Sets Move in Character Controller
        charCon.Move(moveAmount * Time.deltaTime);

        // Crouching
        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame)
        {
            isCrouching = !isCrouching;
        }

        float targetHeight = isCrouching ? crouchHeight : standHeight;
        Vector3 camPos = theCam.transform.localPosition;
        camPos.y = Mathf.Lerp(camPos.y, targetHeight, crouchSpeed * Time.deltaTime);
        theCam.transform.localPosition = camPos;




        //                       ---Pickups---

        // Creates a raycast from the cameras viewport
        Ray ray = theCam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        RaycastHit hit; // Local variable to control raycast hit


        if (heldPickup == null && heldBox == null) // Determines if an object is being held by the player
        {                                          // Only executes if player is NOT holding an object

            // Checks to see if left mouse is clicked
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                // If the ray hits an object within the interaction range and is stock, pickup that object
                if (Physics.Raycast(ray, out hit, interactionRange, whatIsStock))
                {
                    //Debug.Log("I see a pickup");

                    heldPickup = hit.collider.GetComponent<StockObject>(); // Sets the held pickup to the collider that was hit
                    heldPickup.transform.SetParent(holdPoint);  // Sets the transform of the held pickup to be a parent of our hold point empty
                    heldPickup.Pickup();    // Executes pickup function

                    return; // Makes sure we can't double pickup items
                }

                // If the ray hits an object within the interaction range and is a stock box, pickup that object
                if(Physics.Raycast(ray, out hit, interactionRange, whatIsStockBox))
                {

                    // Same logic as above
                    heldBox = hit.collider.GetComponent<StockBoxController>();
                    heldBox.transform.SetParent(boxHoldPoint);
                    heldBox.Pickup();

                    if(heldBox.isOpen == false)
                    {
                        heldBox.OpenClose();
                    }

                    return;

                }
            }

            // Checks to see if right mouse is clicked
            // Allows taking items off shelves
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                // If the ray hits an item in the interaction range and that item is on a shelf, take that item
                if (Physics.Raycast(ray, out hit, interactionRange, whatIsShelf))
                {
                    heldPickup = hit.collider.GetComponent<ShelfSpaceController>().GetStock();

                    if(heldPickup != null)
                    {
                        heldPickup.transform.SetParent(holdPoint);
                        heldPickup.Pickup(); 
                    }
                }
            }

            // Checks to see if the "e" key is pressed
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                // If the ray hits an object in the interaction range and that object is a shelf, open the price update interface
                if (Physics.Raycast(ray, out hit, interactionRange, whatIsShelf))
                {
                    hit.collider.GetComponent<ShelfSpaceController>().StartPriceUpdate();
                }
            
                // If the ray hits an object in the interaction range and that object is a stock box, open/close it
                if (Physics.Raycast(ray, out hit, interactionRange, whatIsStockBox))
                {
                    hit.collider.GetComponent<StockBoxController>().OpenClose();
                }
            }
        }

        else    // Executes if a pickup is being held by the player
        {

            // Only executes if the player is holding a pickup
            if (heldPickup != null)
            {

                // Checks to see if left mouse is clicked
                // Allows object placing
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    // If the object hit by the ray is a shelf, place the held item 
                    if (Physics.Raycast(ray, out hit, interactionRange, whatIsShelf))
                    {

                        hit.collider.GetComponent<ShelfSpaceController>().PlaceStock(heldPickup);

                        if (heldPickup.isPlaced == true)
                        {
                            heldPickup = null;
                        }

                    }

                }

                // Checks to see if right mouse is clicked
                // Allows object dropping
                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    heldPickup.Release();   // Remove kinematics
                    heldPickup.rigBod.AddForce(theCam.transform.forward * throwForce, ForceMode.Impulse); // Applies throw force to drop action 

                    heldPickup.transform.SetParent(null); // Removes hold point parent
                    heldPickup = null; // Unassigns object from the player

                }
            }

            // Only executs when the player is holding a box
            if (heldBox != null)
            {
                // Checks to see if right mouse is clicked
                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    heldBox.Release();   // Remove kinematics
                    heldBox.rigBod.AddForce(theCam.transform.forward * throwForce, ForceMode.Impulse); // Applies throw force to drop action 

                    heldBox.transform.SetParent(null); // Removes hold point parent
                    heldBox = null; // Unassigns object from the player

                }

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    heldBox.OpenClose();
                }

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (Physics.Raycast(ray, out hit, interactionRange, whatIsShelf))
                    {
                        heldBox.PlaceStockOnShelf(hit.collider.GetComponent<ShelfSpaceController>());
                    }
                }
            }

        }
    }
}
