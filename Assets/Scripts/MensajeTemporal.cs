using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensajeTemporal : MonoBehaviour
{
    [SerializeField] GameObject MessageUi;
    bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            isCollected = true;
            MessageUi.SetActive(true);

            // Desactiva el mensaje después de 4 segundos (ajusta el tiempo según tus necesidades)
            Invoke("DisableMessage", 4f);

            // Aquí puedes agregar lógica adicional según tus necesidades
            // Por ejemplo, puedes activar efectos visuales, reproducir sonidos, etc.
        }
    }

    void DisableMessage()
    {
        MessageUi.SetActive(false);
    }
}