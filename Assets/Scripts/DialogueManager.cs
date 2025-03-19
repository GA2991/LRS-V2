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

    private void Start()
    { 
        gameObject.SetActive(false); // Asegura que empiece desactivado 
    }
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

        if (textComponent == null)
        {
            Debug.LogError("El componente TextMeshProUGUI no está asignado en el Inspector del DialogueManager.");
        }
    }


    public void StartDialogue(string[] dialogueLines)
    {
        if (textComponent == null)
        {
            Debug.LogError("El componente TextMeshProUGUI no está asignado en el Inspector.");
            return;
        }

        lines = dialogueLines;
        index = 0;
        textComponent.text = string.Empty;

        // Activa el objeto si está desactivado
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

    StartCoroutine(TypeLine());
    }
    private void Update()
    {
        // Asegúrate de que textComponent y lines estén correctamente configurados 
        if (textComponent == null || lines == null || lines.Length == 0) 
        { 
            return;
        }
        // Avanzar al hacer clic
        if (Input.GetMouseButtonDown(0)) // Botón izquierdo del mouse
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
