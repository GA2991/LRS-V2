using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public int range = 10; // Maximum distance the platform will move
    public bool moveOnXAxis = true; // If true, the platform moves on the X-axis; otherwise, it moves on the Z-axis
    private float initialPosition; // Store the initial position of the platform (X or Z)

    [SerializeField, Min(0.1f)] private int speedPlatform;
    private Rigidbody rb;

    private void Start()
    {
        // Get the Rigidbody component attached to the platform
        rb = GetComponent<Rigidbody>();

        // Store the initial position of the platform based on the selected axis
        initialPosition = moveOnXAxis ? transform.position.x : transform.position.z;
    }

    void FixedUpdate()
    {
        // Calculate the new position using Mathf.PingPong for oscillating movement
        float newPosition = initialPosition + Mathf.PingPong(Time.time * speedPlatform, range) - range / 2f;

        // Move the platform to the new position based on the selected axis
        if (moveOnXAxis)
        {
            rb.MovePosition(new Vector3(newPosition, transform.position.y, transform.position.z));
        }
        else
        {
            rb.MovePosition(new Vector3(transform.position.x, transform.position.y, newPosition));
        }
    }
}