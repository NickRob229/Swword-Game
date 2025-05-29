using UnityEngine;

public class RotateCircle : MonoBehaviour
{
    public float rotationSpeed = 30f; // degrees per second

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}