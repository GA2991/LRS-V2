using System.Collections;
using UnityEngine;

public class WeatherAndDayNightCycle : MonoBehaviour
{
    [Header("Day/Night Cycle Settings")]
    [SerializeField, Range(0, 24)] private float dayDurationInMinutes = 1; // Duration of a full day in minutes
    [SerializeField] private Light directionalLight; // Reference to the sun/moon light
    private float timeOfDay; // Current time of day (0 to 24)

    [Header("Weather Settings")]
    [SerializeField] private ParticleSystem rainEffect; // Rain particle system
    [SerializeField] private ParticleSystem snowEffect; // Snow particle system
    private string currentWeather = "Clear"; // Current weather state

    private void Update()
    {
        UpdateDayNightCycle();
        HandleWeather();
    }

    private void UpdateDayNightCycle()
    {
        // Increment time of day based on real-time
        timeOfDay += (Time.deltaTime / (dayDurationInMinutes * 60)) * 24;
        if (timeOfDay >= 24) timeOfDay = 0;

        // Rotate the directional light to simulate the sun/moon movement
        float lightRotation = (timeOfDay / 24) * 360f - 90f;
        directionalLight.transform.rotation = Quaternion.Euler(lightRotation, 170f, 0f);
    }

    private void HandleWeather()
    {
        // Toggle weather effects based on the current weather
        rainEffect.gameObject.SetActive(currentWeather == "Rain");
        snowEffect.gameObject.SetActive(currentWeather == "Snow");
    }

    public void SetWeather(string weather)
    {
        currentWeather = weather;
    }

    public float GetTimeOfDay()
    {
        return timeOfDay;
    }
}
