using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem; // Automatically added from InputActionReference

public class PlayerController : MonoBehaviour
{
    //                  Public Variables
    // Public variables create an interactable toggle in Unity

    // References Unity input system
    // Controls player movement
    public InputActionReference moveAction;

    // References Character Controller system
    // Controls Unity's CharacterController
    public CharacterController charCon;

    // Controls player movement speed
    public float moveSpeed;

    // Controls player jump
    public InputActionReference jumpAction;

    // Controls player jump force
    public float jumpForce;

    // Controls player look
    public InputActionReference lookAction;

    // Controls player look speed
    public float lookSpeed;

    // Reference to player camera
    public Camera theCam;

    // Controls look limit
    public float minLookAngle, maxLookAngle;

    // LayerMask references Layer system in Unity
    // Creates a dropdown in Unity to assign a layer to this variable
    public LayerMask whatIsStock;

    // Controls the range a player can interact with an object in the game world
    public float interactionRange;

    public Transform holdPoint;




    //                  Private Variables
    // Private variables don't show up in Unity Editor

    // Controls player 
    private float ySpeed;

    // Controls horizontal and vertical rotation
    private float horiRot, vertRot;

    private GameObject heldPickup;




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

        //                          Look
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





        //                        Movement
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


        //                          Pickups
        // Creates a raycast from the cameras viewport
        Ray ray = theCam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        RaycastHit hit; // Local variable to control raycast hit

        // Checks to see if left mouse is clicked
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            // If the ray hits within the interaction range and is stock, handle the following logic
            if (Physics.Raycast(ray, out hit, interactionRange, whatIsStock))
            {
                //Debug.Log("I see a pickup");

                heldPickup = hit.collider.gameObject; // Assigns object to the player
                heldPickup.transform.SetParent(holdPoint); // Sets the transformation to Hold Point empty in Unity
                heldPickup.transform.localPosition = Vector3.zero; // Vector3.zero sets position to (0,0,0)
                heldPickup.transform.localRotation = Quaternion.identity; // Same as Vector3.zero for rotation

                heldPickup.GetComponent<Rigidbody>().isKinematic = true; // Allows object to be held
            }

        }

        // Checks to see if right mouse is clicked
        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            heldPickup.GetComponent<Rigidbody>().isKinematic = false; // Disables Kinematics to drop object

            heldPickup.transform.SetParent(null); // Removes hold point parent
            heldPickup = null; // Unassigns object from the player
        
        }


    }
}
