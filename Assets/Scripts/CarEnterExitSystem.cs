using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnterExitSystem : MonoBehaviour
{
    public MonoBehaviour CarController; // Controlador del coche
    public Transform Car;               // Transform del coche
    public Transform Player;            // Transform del jugador

    [Header("Cameras")]
    public GameObject PlayerCam;        // Cámara del jugador
    public GameObject CarCam;           // Cámara del coche

    [SerializeField] GameObject DriveUi; // UI de "Conducir"

    private bool canDrive;              // ¿El jugador puede interactuar con el coche?
    private bool isDriving = false;     // ¿El jugador está conduciendo?

    void Start()
    {
        CarController.enabled = false;          // Desactiva el controlador del coche al inicio
        DriveUi.gameObject.SetActive(false);    // Oculta el UI al inicio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canDrive) // Usar la tecla E para interactuar
        {
            if (isDriving)
            {
                ExitCar(); // Salir del coche
            }
            else
            {
                EnterCar(); // Entrar al coche
            }
        }
    }

    private void EnterCar()
    {
        isDriving = true;
        CarController.enabled = true;          // Activa el controlador del coche
        DriveUi.gameObject.SetActive(false);   // Oculta el UI
        Player.transform.SetParent(Car);       // Hace al jugador hijo del coche
        Player.gameObject.SetActive(false);    // Oculta al jugador
        PlayerCam.gameObject.SetActive(false); // Activa la cámara del coche
        CarCam.gameObject.SetActive(true);
    }

    private void ExitCar()
    {
        isDriving = false;
        CarController.enabled = false;         // Desactiva el controlador del coche
        Player.transform.SetParent(null);      // Separa al jugador del coche
        Player.gameObject.SetActive(true);     // Muestra al jugador
        PlayerCam.gameObject.SetActive(true);  // Activa la cámara del jugador
        CarCam.gameObject.SetActive(false);
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            DriveUi.gameObject.SetActive(true); // Mostrar el UI cuando el jugador está cerca
            canDrive = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            DriveUi.gameObject.SetActive(false); // Ocultar el UI al alejarse
            canDrive = false;
        }
    }
}
