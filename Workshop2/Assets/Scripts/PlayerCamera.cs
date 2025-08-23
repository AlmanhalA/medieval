using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform Player;
    public Transform cameraPivot;
    public float mouseSensitivity = 2.0f;
    public float smoothSpeed = 10f;
    public float topAngleLimit = 60.0f;
    public float bottomAngleLimit = -60.0f;
    public float cameraDistance = 3.0f;
    public Vector3 positionOffset = new Vector3(0.7f, 0.0f, 0f);
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Start()
    {
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //mouse input
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Clamp the vertical rotation (pitch) to prevent the camera from flipping over
        pitch = Mathf.Clamp(pitch, bottomAngleLimit, topAngleLimit);

        // --- Calculate Rotation and Position ---
        // Calculate the desired rotation of the camera
        Quaternion desiredRotation = Quaternion.Euler(pitch, yaw, 0);
        
        // Determine the desired position of the camera without considering collisions yet.
        // Start from the pivot, apply the rotation, add the over-the-shoulder offset, and move back by the distance.
        Vector3 desiredPosition = cameraPivot.position + desiredRotation * (positionOffset - new Vector3(0, 0, cameraDistance));

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        transform.rotation = desiredRotation;
    }
}
