using UnityEngine;

public class ShoulderCameraFollow : MonoBehaviour
{
    public Transform target; // Player to follow
    public Vector3 shoulderOffset = new Vector3(-1.5f, 2f, -3f); // Camera position relative to player
    public float followSpeed = 7f;

    [Header("Look Settings")]
    public float lookDownAngle = 15f;           // Slight tilt downwards
    public float rotationSmoothSpeed = 5f;      // How smoothly camera rotates

    private void LateUpdate()
    {
        if (target == null) return;

        // 1. Get offset relative to player rotation
        Vector3 desiredPosition = target.TransformPoint(shoulderOffset);

        // 2. Smoothly move camera to desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // 3. Make camera look where player is facing, with tilt
        Vector3 lookDirection = target.forward;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        Quaternion tiltRotation = Quaternion.Euler(lookDownAngle, lookRotation.eulerAngles.y, 0);

        // 4. Smoothly rotate camera toward that direction
        transform.rotation = Quaternion.Slerp(transform.rotation, tiltRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}
