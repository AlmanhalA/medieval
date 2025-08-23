using UnityEngine;

// This script requires a 3D Rigidbody component.
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    public Transform cameraTransform;
    public float rotationSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; 
        isGrounded = true; 
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
   void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        // Only move and rotate if there is input.
        if (moveDirection.magnitude >= 0.1f)
        {
            // --- Rotation ---
            // Calculate the angle the player needs to turn to face the movement direction relative to the camera.
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            // Smoothly dampen the rotation angle to avoid a snapping motion.
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
            // Apply the calculated rotation to the player.
            rb.MoveRotation(Quaternion.Euler(0f, angle, 0f));

            // --- Movement ---
            // Calculate the movement direction based on the target rotation.
            Vector3 moveVector = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            // Apply movement using transform.Translate. We use Time.fixedDeltaTime because we are in FixedUpdate.
            transform.Translate(moveVector.normalized * moveSpeed * Time.fixedDeltaTime, Space.World);
        }
        else
        {
            // If there's no input, stop horizontal movement to prevent sliding.
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    /// <summary>
    /// Handles the physics of making the player jump.
    /// </summary>


    private void Jump()
    {
        // Reset vertical velocity to ensure consistent jump height.
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        // Apply an upward force.
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
