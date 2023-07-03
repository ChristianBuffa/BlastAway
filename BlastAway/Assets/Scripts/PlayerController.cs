using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    
    private Camera playerCamera;
    private CharacterController controller;
    private float xRotation;

    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        SimulateGravity();
        LookRotation();
        GetMovementInput();
    }

    private void LookRotation()
    {
        var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void GetMovementInput()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var move = transform.right * x + transform.forward * z;

        controller.Move(move * (speed * Time.deltaTime));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void SimulateGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
