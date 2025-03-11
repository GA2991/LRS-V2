using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    private string[] lines;
    private int index;
    private bool isTyping = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(string[] dialogueLines)
    {
        lines = dialogueLines;
        index = 0;
        textComponent.text = string.Empty;
        gameObject.SetActive(true); // Activa el Canvas al iniciar el diálogo
        StartCoroutine(TypeLine());
    }

    private void Update()
    {
        // Avanzar al hacer clic
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) // Botón izquierdo del mouse
        {
            if (!isTyping && textComponent.text == lines[index])
            {
                NextLine();
            }
            else if (isTyping)
            {
                // Completa la línea al instante si está escribiendo
                StopAllCoroutines();
                textComponent.text = lines[index];
                isTyping = false;
            }
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false); // Desactiva el Canvas cuando termina el diálogo
    }
}
