using UnityEngine;

public class MotorSound : MonoBehaviour
{
    public AudioSource motorSound;
    public AudioSource crashSound;
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;
    public float pitchMultiplier = 0.01f;
    public float maxSpeed = 10f;
    public float collisionCooldown = 0.5f; // Nuevo parámetro para el tiempo de espera entre colisiones

    private Rigidbody rb;
    private float lastCollisionTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastCollisionTime = -collisionCooldown; // Inicializa el temporizador para permitir la primera reproducción
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;

        // Ajusta la frecuencia del sonido según la velocidad del vehículo
        float pitch = Mathf.Clamp(speed * pitchMultiplier, minPitch, maxPitch);
        motorSound.pitch = pitch;

        // Ajusta el volumen del sonido según la velocidad del vehículo
        motorSound.volume = Mathf.Clamp01(speed / maxSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si ha pasado el tiempo de espera desde la última colisión
        if (Time.time - lastCollisionTime > collisionCooldown && collision.relativeVelocity.magnitude > 7f)
        {
            // Reproduce el sonido de choque y actualiza el tiempo de la última colisión
            crashSound.Play();
            lastCollisionTime = Time.time;
        }
    }
}
