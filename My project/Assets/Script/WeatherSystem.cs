using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeatherState { Rain, Snow, Sand }

[RequireComponent(typeof(AudioSource))]
public class WeatherSystem : MonoBehaviour
{
    public WeatherState weatherState;
    public WeatherData[] weatherData;
    public AudioSource audioSource;

    public void SwitchWeather(int switchWeather)
    {
        ResetWeather();

        switch (switchWeather)
        {
            case 0:
                weatherState = WeatherState.Rain;
                break;
            case 1:
                weatherState = WeatherState.Snow;
                break;
            case 2:
                weatherState = WeatherState.Sand;
                break;
        }

    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = 0f;
    }

    private void Start()
    {
        LoadWeatherSystem();
        StartCoroutine(StartDynamicWeather());
        foreach (var switcher in GameObject.FindGameObjectsWithTag("WeatherSwitch"))
        {
            switcher.GetComponent<WeatherSwitch>().BiomSwitched += SwitchWeather; ;
        }
    }

    private void LoadWeatherSystem()
    {
        for (int i = 0; i < weatherData.Length; i++)
        {
            weatherData[i].emission = weatherData[i].particleSystem.emission;
        }
    }

    private IEnumerator StartDynamicWeather()
    {
        while (true)
        {
            switch (weatherState)
            {
                case WeatherState.Rain:
                    ActivateWeather("Rain");
                    break;
                case WeatherState.Snow:
                    ActivateWeather("Snow");
                    break;
                case WeatherState.Sand:
                    ActivateWeather("Sand");
                    break;
            }
            yield return null;
        }
    }

    private void ActivateWeather(string weather)
    {
        if (weatherData.Length > 0)
        {
            for (int i = 0; i < weatherData.Length; i++)
            {
                if (weatherData[i].particleSystem == null)
                {
                    continue;
                }
                if (weatherData[i].name != weather)
                {
                    continue;
                }
                weatherData[i].emission.enabled = true;

                RenderSettings.fogColor = weatherData[i].fogColor;
                ChangeWeatherSettings(weatherData[i].lightIntensity, weatherData[i].weatherAudio, weatherData[i].audioVolume, weatherData[i].skyBox, weatherData[i].fogDensity);
            }
        }
    }

    private void ChangeWeatherSettings(float lightIntensity, AudioClip audioClip, float audioVolume, Material skyBox, float fogDensity)
    {
        // LightIntensity change
        Light tmpLight = GetComponent<Light>();

        if (tmpLight.intensity > lightIntensity)
        {
            tmpLight.intensity -= Time.deltaTime * lightIntensity;
        }
        if (tmpLight.intensity < lightIntensity)
        {
            tmpLight.intensity += Time.deltaTime * lightIntensity;
        }


        // Volume change
        AudioSource tmpAudio = GetComponent<AudioSource>();

        if (tmpAudio.volume > 0 && tmpAudio.clip != audioClip)
        {
            tmpAudio.volume -= Time.deltaTime * .2f;
        }

        if (tmpAudio.volume == 0)
        {
            tmpAudio.Stop();
            tmpAudio.clip = audioClip;
            tmpAudio.loop = true;
            tmpAudio.volume = audioVolume;
            tmpAudio.Play();
        }

        if (tmpAudio.volume < 1 && tmpAudio.clip != audioClip)
        {
            tmpAudio.volume -= Time.deltaTime * .01f;
        }

        // Skybox change
        if (RenderSettings.skybox != skyBox)
        {
            RenderSettings.skybox = skyBox;
        }

        // Fog density change
        if (RenderSettings.fogDensity > fogDensity)
        {
            RenderSettings.fogDensity -= Time.deltaTime * fogDensity;
        }
        if (RenderSettings.fogDensity < fogDensity)
        {
            RenderSettings.fogDensity += Time.deltaTime * fogDensity;
        }
    }

    private void ResetWeather()
    {
        if (weatherData.Length > 0)
        {
            for (int i = 0; i < weatherData.Length; i++)
            {
                if (weatherData[i].emission.enabled)
                {
                    weatherData[i].emission.enabled = false;
                }
            }
        }
    }
}
