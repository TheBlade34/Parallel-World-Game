using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 10f; // Acceleration rate
    public float maxSpeed = 5f; // Maximum movement speed
    public float deceleration = 10f; // Deceleration rate
    public float jumpForce = 10f; // Jump force
    public Camera orthoCamera; // Reference to the orthographic camera
    public Camera perspectiveCamera; // Reference to the perspective camera
    public float cameraFollowSpeed = 5f; // Camera follow speed

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 movementInput;
    private Vector3 currentVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Player movement input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        movementInput = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Apply movement
        ApplyMovement();
    }

    void ApplyMovement()
    {
        // Calculate target velocity based on input and max speed
        Vector3 targetVelocity = movementInput * maxSpeed;

        // Smoothly accelerate/decelerate to target velocity
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * (movementInput.magnitude > 0 ? acceleration : deceleration));

        // Apply velocity to the Rigidbody
        rb.velocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z);

        // Update camera position to match player with interpolation
        Vector3 targetCameraPos = new Vector3(transform.position.x, transform.position.y, orthoCamera.transform.position.z);
        orthoCamera.transform.position = Vector3.Lerp(orthoCamera.transform.position, targetCameraPos, Time.deltaTime * cameraFollowSpeed);

        // Adjust the height of the perspective camera
        float perspectiveHeightOffset = 5.0f; // Adjust this value as needed
        targetCameraPos = new Vector3(transform.position.x, transform.position.y + perspectiveHeightOffset, perspectiveCamera.transform.position.z);
        perspectiveCamera.transform.position = Vector3.Lerp(perspectiveCamera.transform.position, targetCameraPos, Time.deltaTime * cameraFollowSpeed);
    }


    void OnCollisionEnter(Collision collision)
    {
        // Check if collision is with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if collision with the ground ends
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}