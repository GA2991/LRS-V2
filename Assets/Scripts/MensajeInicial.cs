using UnityEngine;

public class MensajeInicial : MonoBehaviour
{
    public GameObject mensajeInicial;

    void Start()
    {
        // Invocar el m�todo MostrarMensajeInicial despu�s de 1 segundos
        Invoke(nameof(MostrarMensajeInicial), 1f);

        // Desactivar el mensaje despu�s de 6 segundos (1 segundos para mostrar + 5 segundos de duraci�n)
        Invoke(nameof(OcultarMensajeInicial), 6f);
    }

    // M�todo para activar el mensaje inicial
    public void MostrarMensajeInicial()
    {
        mensajeInicial.SetActive(true);
    }

    // M�todo para ocultar el mensaje inicial
    public void OcultarMensajeInicial()
    {
        mensajeInicial.SetActive(false);
    }
}
