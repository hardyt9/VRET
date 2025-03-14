using UnityEngine;

public class MoveWithWASD : MonoBehaviour
{
    public float speed = 3.0f; // Movement speed
    public float rotationSpeed = 700.0f; // Rotation speed
    public float gravity = -9.81f; // Gravity strength
    public float jumpHeight = 1.0f; // Jump height (optional)
    public float lookSpeedX = 2.0f; // Mouse look speed on X-axis (Horizontal)
    public float lookSpeedY = 2.0f; // Mouse look speed on Y-axis (Vertical)

    private Transform cameraTransform; // Reference to the camera transform
    private CharacterController characterController; // Reference to the CharacterController
    private Vector3 velocity; // Player's velocity
    private float rotationX = 0.0f; // Current X rotation for vertical look

    void Start()
    {
        // Find the camera within the XR Rig, assuming it's a child of the XR Rig
        cameraTransform = Camera.main.transform;
        
        // Get the CharacterController attached to the player
        characterController = GetComponent<CharacterController>();
        
        // Set the slope limit to allow stairs
        characterController.slopeLimit = 45f; // Ensure this value is sufficient for stairs
        characterController.skinWidth = 0.08f; // Increase skin width to prevent falling through floors
    }

    void Update()
    {
        // Get WASD input
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow

        // Calculate movement direction relative to the camera's rotation
        Vector3 forward = cameraTransform.forward;
        forward.y = 0; // Ignore vertical movement for direction
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0; // Ignore vertical movement for direction
        right.Normalize();

        // Move in the direction of the input
        Vector3 moveDirection = (forward * moveZ + right * moveX) * speed;

        // Apply gravity and jumping if grounded
        if (characterController.isGrounded)
        {
            // Apply a small downward force when grounded to prevent floating
            velocity.y = -2f; // Increased value to ensure proper grounding

            // Optional: Add a jump mechanic
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            // Apply gravity when in the air
            velocity.y += gravity * Time.deltaTime;
        }

        // Move the player horizontally (X/Z axis)
        characterController.Move(moveDirection * Time.deltaTime);

        // Apply vertical velocity (gravity/jump)
        characterController.Move(velocity * Time.deltaTime);

        // Rotate horizontally with mouse (left/right)
        float rotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotation);

        // Rotate vertically with mouse (up/down) for camera
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f); // Clamp to prevent excessive up/down rotation
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
