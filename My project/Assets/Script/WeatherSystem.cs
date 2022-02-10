using System.Collections;
using UnityEngine;

public enum WeatherState { Rain, Snow, Sun }

[RequireComponent(typeof(AudioSource))]
public class WeatherSystem : MonoBehaviour
{ 
    private int switchWeather = 0;

    public AudioSource audioSource;
    public Light sunLight;
    public Transform windzone;

    public WeatherState weatherState;
    public WeatherData[] weatherData;

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
    }
    
    void LoadWeatherSystem()
    {
        for (int i = 0; i < weatherData.Length; i++)
        {
            weatherData[i].emission = weatherData[i].particleSystem.emission;
        } 
    }
    IEnumerator StartDynamicWeather()
    {
        while (true)
        {
            if (weatherState == WeatherState.Rain)
                ActivateWeather("Rain");
            else if (weatherState == WeatherState.Snow)
                ActivateWeather("Snow");
            else if (weatherState == WeatherState.Sun)
                ActivateWeather("Sun");
            yield return null;
        }
    }
    void ActivateWeather(string weather)
    {
        if(weatherData.Length > 0)
        {
            for (int i = 0; i < weatherData.Length; i++)
            {
                if(weatherData[i].particleSystem != null)
                {
                    if(weatherData[i].name == weather)
                    {
                        weatherData[i].emission.enabled = true;
                        weatherData[i].fogColor = RenderSettings.fogColor;
                        RenderSettings.fogColor = Color.Lerp(weatherData[i].currentFogColor, weatherData[i].fogColor, weatherData[i].fogChangeSpeed * Time.deltaTime);

                        ChangeWeatherSettings(weatherData[i].lightIntensity, weatherData[i].weatherAudio, weatherData[i].skyBox);
                    }
                }
            }
        }
    }
    public void SwitchWeather()
    {
        if (switchWeather < 2)
            switchWeather++;
        else switchWeather = 0;
        ResetWeather();

        if (switchWeather == 0)
            weatherState = WeatherState.Rain;
        else if (switchWeather == 1)
            weatherState = WeatherState.Snow;
        else if (switchWeather == 2)
            weatherState = WeatherState.Sun;
        
    }
    void ChangeWeatherSettings(float lightIntensity, AudioClip audioClip, Material skyBox)
    {
        // LightIntensity change
        Light tmpLight = GetComponent<Light>();

        if (tmpLight.intensity > lightIntensity) { tmpLight.intensity -= Time.deltaTime * lightIntensity; }
        if (tmpLight.intensity < lightIntensity) { tmpLight.intensity += Time.deltaTime * lightIntensity; }
        // Volume change
        if (weatherData[switchWeather].useAudio == true)
        {
            AudioSource tmpAudio = GetComponent<AudioSource>();
            
            if(tmpAudio.volume > 0 && tmpAudio.clip != audioClip)
            {
                tmpAudio.volume -= Time.deltaTime * .2f;
            }

            if(tmpAudio.volume == 0)
            {
                tmpAudio.Stop();
                tmpAudio.clip = audioClip;
                tmpAudio.loop = true;
                tmpAudio.volume = weatherData[switchWeather].audioVolume;
                tmpAudio.Play();
            }

            if(tmpAudio.volume < 1 && tmpAudio.clip != audioClip)
            {
                tmpAudio.volume -= Time.deltaTime * .01f;
            }
        }
        // Skybox change
        if (RenderSettings.skybox != skyBox)
            RenderSettings.skybox = skyBox;

    }
    void ResetWeather()
    {
        if(weatherData.Length > 0)
        {
            for (int i = 0; i < weatherData.Length; i++)
            {
                if(weatherData[i].emission.enabled != false)
                {
                    weatherData[i].emission.enabled = false;
                }
            }
        }
    }
}
