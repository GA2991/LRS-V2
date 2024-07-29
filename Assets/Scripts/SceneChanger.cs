using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    
    private string nombreEscena = "Level 2";

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entró en el collider es el jugador
        if (other.gameObject.tag == "Player")
        {
            // Cambia a la escena "Ciudad" cuando el jugador entra en el collider del objeto
            SceneManager.LoadScene(nombreEscena);
        }
    }
}
