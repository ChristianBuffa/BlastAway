using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private bool canMove = true;
    public bool CanMove {
        get => canMove;
        set => canMove = value;
    }
    // public bool CanMove { get ; private set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintkey);
    private bool ShouldJump => controller.isGrounded && Input.GetButtonDown("Jump");
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouch && controller.isGrounded;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintkey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.C;

    [Header("Functional Options")] 
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canHeadBob = true;

    [Header("Movement Parameters")] 
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float crouchSpeed = 1.5f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80f;

    [Header("Jumping Parameters")] 
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Crouch Parameters")] 
    [SerializeField] private float crouchingHeight = 0.5f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter;
    private Vector3 standingCameraPosition;
    private bool isCrouching;
    private bool duringCrouch;

    [Header("Headbob Parameters")] 
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.1f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos;
    private float timer;

    private Camera playerCamera;
    private CharacterController controller;

    private Vector2 currentInput;
    private Vector3 moveDirection;

    private float rotationX;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();
        defaultYPos = playerCamera.transform.localPosition.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        standingCenter = controller.center;
        standingCameraPosition = playerCamera.transform.localPosition;
    }

    private void Update()
    {
        if (CanMove)
        {
            HandleMouseLook();
            HandleMovementInput();

            if (canJump)
                HandleJump();

            if (canCrouch)
                HandleCrouch();

            if (canHeadBob)
                HandleHeadBob();

            ApplyFinalMovement();
        }
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleMovementInput()
    {
        currentInput =
            new Vector2((isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"),
                (isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        var moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) +
                        (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleJump()
    {
        if (ShouldJump)
            moveDirection.y = jumpForce;
    }

    private void HandleCrouch()
    {
        if (ShouldCrouch)
            StartCoroutine(CrouchCoroutine());
    }

    private IEnumerator CrouchCoroutine()
    {
        if(isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break;
        
        duringCrouch = true;

        var timeElapsed = 0f;
        var targetHeight = isCrouching ? standingHeight : crouchingHeight;
        var currentHeight = controller.height;
        var targetCenter = isCrouching ? standingCenter : crouchingCenter;
        var currentCenter = controller.center;
        var targetCameraPosition = isCrouching ? standingCameraPosition : crouchingCenter;

        while (timeElapsed < timeToCrouch)
        {
            controller.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, targetCameraPosition, timeElapsed / timeToCrouch);
            controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        controller.center = targetCenter;
        controller.height = targetHeight;
        playerCamera.transform.localPosition = targetCameraPosition;
        defaultYPos = playerCamera.transform.localPosition.y;

        isCrouching = !isCrouching;

        duringCrouch = false;
        yield return null;
    }

    private void HandleHeadBob()
    {
        if (!controller.isGrounded) return;

        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) *
                (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z);
        }
    }

    private void ApplyFinalMovement()
    {
        if (!controller.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }
}