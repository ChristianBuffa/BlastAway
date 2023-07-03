using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    
    [Header("Controls")] 
    [SerializeField] private KeyCode sprintkey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.C;
    
    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float sprintMultiplier = 2f;
    
    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80f;
    
    [Header("Jumping Parameters")]
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravity = 9.81f;

    private Camera playerCamera;
    private CharacterController controller;
    
    private Vector2 currentInput;
    private Vector3 moveDirection;
    
    private float rotationX;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Update()
    {
        if (CanMove)
        {
            LookRotation();
            HandleMovement();
            ApplyFinalMovement();
        }
    }

    private void LookRotation()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleMovement()
    {
        currentInput = new Vector2(walkSpeed * Input.GetAxis("Vertical"), walkSpeed * Input.GetAxis("Horizontal"));

        var moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) +
                        (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void ApplyFinalMovement()
    {
        if (!controller.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
