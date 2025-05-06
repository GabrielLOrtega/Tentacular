
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpPower = 7f;
    public float gravity = 20f;
    public float lookSpeed = 2f;
    public float lookXLimit = 90f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    public float crouchTransitionSpeed = 15f; // Speed of crouch height transition
    public float stepHeight = 0.3f; // Maximum height of a step the player can climb
    public float stepSmooth = 2f; // Smoothness of stepping up

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;
    private bool isCurrentlyRunning = false; // Tracks running status
    private float targetHeight; // Target height for crouching/standing

    // GameObjects for raycasts
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set initial height
        targetHeight = defaultHeight;

        // Initialize raycast positions
        if (stepRayUpper != null)
        {
            stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
        }
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Determine if the player is grounded
        bool isGrounded = characterController.isGrounded;

        // Crouch logic (press and hold)
        if (Input.GetKey(KeyCode.LeftControl) && canMove)
        {
            targetHeight = crouchHeight;
        }
        else
        {
            targetHeight = defaultHeight;
        }

        // Smoothly adjust height
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, crouchTransitionSpeed * Time.deltaTime);

        // Adjust speed while crouching
        bool isCrouching = Mathf.Abs(characterController.height - crouchHeight) < 0.1f;
        if (isCrouching)
        {
            isCurrentlyRunning = false; // Cannot run while crouching
        }

        // Determine movement speed
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded && !isCrouching)
        {
            isCurrentlyRunning = true; // Start running if grounded and shift is held
        }
        else if (isGrounded)
        {
            isCurrentlyRunning = false; // Stop running if grounded and shift is not held
        }

        float currentSpeed = isCrouching ? crouchSpeed : (isCurrentlyRunning ? runSpeed : walkSpeed);
        float curSpeedX = canMove ? currentSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? currentSpeed * Input.GetAxis("Horizontal") : 0;

        // Preserve Y-axis movement
        float movementDirectionY = moveDirection.y;

        if (isGrounded)
        {
            // Update movement direction when grounded
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        }
        else
        {
            // Maintain momentum while airborne
            Vector3 horizontalVelocity = new Vector3(moveDirection.x, 0, moveDirection.z);
            Vector3 inputVelocity = (forward * curSpeedX) + (right * curSpeedY);

            // Add input to current horizontal velocity
            if (inputVelocity != Vector3.zero)
            {
                horizontalVelocity = inputVelocity;
            }

            moveDirection = horizontalVelocity;
        }

        // Apply Y-axis movement
        moveDirection.y = movementDirectionY;

        // Jump logic
        if (Input.GetButton("Jump") && canMove && isGrounded)
        {
            moveDirection.y = jumpPower;
        }

        // Apply gravity if not grounded
        if (!isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Stair stepping logic
        if (isGrounded)
        {
            StepClimb();
        }

        // Move the character
        characterController.Move(moveDirection * Time.deltaTime);

        // Handle camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    void StepClimb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
            {
                characterController.Move(Vector3.up * stepSmooth * Time.deltaTime);
            }
        }

        RaycastHit hitLower45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(new Vector3(1.5f, 0, 1)), out hitLower45, 0.1f))
        {
            RaycastHit hitUpper45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(new Vector3(1.5f, 0, 1)), out hitUpper45, 0.2f))
            {
                characterController.Move(Vector3.up * stepSmooth * Time.deltaTime);
            }
        }

        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(new Vector3(-1.5f, 0, 1)), out hitLowerMinus45, 0.1f))
        {
            RaycastHit hitUpperMinus45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(new Vector3(-1.5f, 0, 1)), out hitUpperMinus45, 0.2f))
            {
                characterController.Move(Vector3.up * stepSmooth * Time.deltaTime);
            }
        }
    }
}
