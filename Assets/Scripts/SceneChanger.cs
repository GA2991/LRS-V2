using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    
    private int escenaActual;
    void Start()
    {
        // Obtiene el índice de la escena actual
        escenaActual = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        int siguienteEscena = escenaActual + 1;
        // Comprueba si el objeto que entró en el collider es el jugador
        if (other.gameObject.tag == "Player")
        {
            // Cambia a la escena "Ciudad" cuando el jugador entra en el collider del objeto
            SceneManager.LoadScene(siguienteEscena);
        }
    }
}
