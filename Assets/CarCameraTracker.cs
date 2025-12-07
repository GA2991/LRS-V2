using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraTracker : MonoBehaviour
{
    public Transform car;       // referencia al coche
    public Vector3 offset;      // posición relativa detrás/arriba del coche
    public bool copyYawOnly = true;

    void LateUpdate()
    {
        // Seguir la posición del coche con offset
        transform.position = car.position + offset;

        if (copyYawOnly)
        {
            // Copiar solo la rotación en Y
            float yaw = car.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yaw, 0);
        }
        else
        {
            // Copiar toda la rotación del coche (si lo necesitas)
            transform.rotation = car.rotation;
        }
    }
}
