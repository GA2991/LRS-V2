using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnterExitSystem : MonoBehaviour
{
    public PrometeoCarController carControllerScript; // Referencia al script del controlador del coche
    public Transform Car;               // Transform del coche
    public Transform Player;            // Transform del jugador

    [Header("Cameras")]
    public GameObject PlayerCam;        // Cámara del jugador
    public GameObject CarCam;           // Cámara del coche

    [SerializeField] GameObject DriveUi; // UI de "Conducir"

    public AudioClip carDoorSound;       // Sonido de abrir/cerrar la puerta del coche

    private bool canDrive;              // ¿El jugador puede interactuar con el coche?
    private bool isDriving = false;     // ¿El jugador está conduciendo?

    void Start()
    {
        if (carControllerScript == null || DriveUi == null || Car == null || Player == null || PlayerCam == null || CarCam == null)
        {
            Debug.LogError("One or more required components are not assigned in the inspector.");
            enabled = false;
            return;
        }

        carControllerScript.enabled = false;   // Desactiva el controlador del coche al inicio
        DriveUi.gameObject.SetActive(false);   // Oculta el UI al inicio
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
        carControllerScript.enabled = true;   
        AudioSource.PlayClipAtPoint(carDoorSound, Car.position); // Activa el script del controlador del coche
        DriveUi.gameObject.SetActive(false);   // Oculta el UI
        Player.transform.SetParent(Car);       // Hace al jugador hijo del coche
        Player.gameObject.SetActive(false);    // Oculta al jugador
        PlayerCam.gameObject.SetActive(false); // Activa la cámara del coche
        CarCam.gameObject.SetActive(true);
        StopAllCoroutines(); // cancela cualquier WaitForCarToStop activo


        Rigidbody rb = Car.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false; // activar físicas al conducir
    }

    private void ExitCar()
    {
        isDriving = false;
        carControllerScript.ThrottleOff();     // Detiene la aceleración del coche
        carControllerScript.enabled = false;   // Desactiva el script del controlador del coche
                         // Detiene el movimiento del coche
        AudioSource.PlayClipAtPoint(carDoorSound, Car.position);
        Player.transform.SetParent(null);      // Separa al jugador del coche
        Player.gameObject.SetActive(true);     // Muestra al jugador
        PlayerCam.gameObject.SetActive(true);  // Activa la cámara del jugador
        CarCam.gameObject.SetActive(false);

        Rigidbody rb = Car.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // No lo ponemos kinematic de inmediato, dejamos que se frene naturalmente
            StartCoroutine(WaitForCarToStop(rb));
        }
        
    }

    private IEnumerator WaitForCarToStop(Rigidbody rb)
{
    float stillTime = 0f;

    while (!isDriving)
    {
        if (rb.velocity.magnitude < 0.5f)
        {
            stillTime += Time.deltaTime;
            if (stillTime > 2f) // 2 segundos quieto
            {
                rb.isKinematic = true;
                yield break;
            }
        }
        else
        {
            stillTime = 0f; // se resetea si vuelve a moverse
        }
        yield return null;
    }
}


    private void StopCarMovement()
    {
        Rigidbody carRigidbody = Car.GetComponent<Rigidbody>();
        if (carRigidbody != null)
        {
            carRigidbody.velocity = Vector3.zero;
            carRigidbody.angularVelocity = Vector3.zero;
            carRigidbody.Sleep();
        }
    }


    private void ApplyBrakes(Rigidbody carRigidbody)
    {
        carRigidbody.AddForce(-carRigidbody.velocity * 100f, ForceMode.Acceleration); // Aumenta la fuerza de frenado
        carRigidbody.AddTorque(-carRigidbody.angularVelocity * 100f, ForceMode.Acceleration); // Aumenta la fuerza de frenado angular
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