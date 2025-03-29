using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public int range = 10; // Maximum distance the platform will move horizontally
    private float initialX; // Store the initial X position of the platform

    private Rigidbody rb;

    private void Start()
    {
        // Get the Rigidbody component attached to the platform
        rb = GetComponent<Rigidbody>();

        // Store the initial X position of the platform
        initialX = transform.position.x;
    }

    void FixedUpdate()
    {
        // Calculate the new position using Mathf.PingPong for oscillating movement
        float newX = initialX + Mathf.PingPong(Time.time * 2, range) - range / 2f;

        // Move the platform to the new position
        rb.MovePosition(new Vector3(newX, transform.position.y, transform.position.z));
    }
}