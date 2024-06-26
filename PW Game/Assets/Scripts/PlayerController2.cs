using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 5f;
    public float deceleration = 10f;
    public float jumpForce = 10f;
    public Camera orthoCamera;
    public float cameraFollowSpeed = 5f;
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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        moveVertical = 0f;
        movementInput = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void ApplyMovement()
    {
        Vector3 targetVelocity = movementInput * maxSpeed;
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * (movementInput.magnitude > 0 ? acceleration : deceleration));
        rb.velocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z);
        Vector3 targetCameraPos = new Vector3(transform.position.x, transform.position.y, orthoCamera.transform.position.z);
        orthoCamera.transform.position = Vector3.Lerp(orthoCamera.transform.position, targetCameraPos, Time.deltaTime * cameraFollowSpeed);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
