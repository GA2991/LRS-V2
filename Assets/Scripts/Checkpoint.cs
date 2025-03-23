using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject player;
    private Vector3 vectorPoint;

    [SerializeField] GameObject MessageUi;
    private bool touchCheck = false;

    void Start()
    {
        // Find the player object by tag
        player = GameObject.FindWithTag("Player"); // Assuming the player has a tag "Player"
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        // Initially hide the message UI
        MessageUi.SetActive(false);
    }

    void Update()
    {
        // Check if the 'L' key is pressed and the player has touched the checkpoint
        if (Input.GetKeyDown(KeyCode.L) && touchCheck)
        {
            // Move the player to the checkpoint position with an offset
            player.transform.position = vectorPoint + new Vector3(2, 4, 2);
            Debug.Log("Checkpoint activated");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the checkpoint trigger
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                // Save the player's current position as the checkpoint
                vectorPoint = player.transform.position;
                touchCheck = true;
                MessageUi.SetActive(true);

                // Disable the message after 4 seconds (adjust the time as needed)
                Invoke("DisableMessage", 4f);

                // Deactivate touchCheck on all other checkpoints
                DeactivateOtherCheckpoints();
            }
            else
            {
                Debug.LogError("Player object is null in OnTriggerEnter.");
            }
        }
    }

    void DisableMessage()
    {
        // Hide the message UI
        MessageUi.SetActive(false);
    }

    void DeactivateOtherCheckpoints()
    {
        // Find all checkpoints in the scene
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();

        // Deactivate touchCheck on all other checkpoints
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (checkpoint != this)
            {
                checkpoint.touchCheck = false;
            }
        }
    }
}