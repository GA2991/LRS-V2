using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey = KeyCode.E;
    public UnityEvent interactAction;

    [Header("Dialogue Settings")]
    public string[] dialogueLines; // Líneas de diálogo específicas para este objeto

    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
                TriggerDialogue();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player in range");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player not in range");

            // Cierra el diálogo al salir del rango
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.EndDialogue();
            }
        }
    }
    //
    private void TriggerDialogue()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(dialogueLines);
        }
        else
        {
            Debug.LogError("No se encontró el DialogueManager en la escena.");
        }
    }
}
