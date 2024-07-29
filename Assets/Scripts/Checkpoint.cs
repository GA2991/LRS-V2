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
        player = GameObject.FindWithTag("Player"); // Assuming the player has a tag "Player"
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        MessageUi.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && touchCheck)
        {
            player.transform.position = vectorPoint + new Vector3(2, 4, 2);
            Debug.Log("Checkpoint activated");
        }
    }

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        if (player != null)
        {
            vectorPoint = player.transform.position;
            touchCheck = true;
            MessageUi.SetActive(true);

            // Disable the message after 4 seconds (adjust the time as needed)
            Invoke("DisableMessage", 4f);
        }
        else
        {
            Debug.LogError("Player object is null in OnTriggerEnter.");
        }
    }
}


    void DisableMessage()
    {
        MessageUi.SetActive(false);
    }
}
