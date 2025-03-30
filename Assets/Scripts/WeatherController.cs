using UnityEngine;

public class WeatherController : MonoBehaviour
{
    private WeatherAndDayNightCycle weatherSystem;

    private void Start()
    {
        weatherSystem = FindObjectOfType<WeatherAndDayNightCycle>();
    }

    private void Update()
    {
        // Example: Change weather to Rain when pressing the R key
        if (Input.GetKeyDown(KeyCode.R))
        {
            weatherSystem.SetWeather("Rain");
        }

        // Example: Change weather to Snow when pressing the S key
        if (Input.GetKeyDown(KeyCode.S))
        {
            weatherSystem.SetWeather("Snow");
        }

        // Example: Clear weather when pressing the C key
        if (Input.GetKeyDown(KeyCode.C))
        {
            weatherSystem.SetWeather("Clear");
        }
    }
}
