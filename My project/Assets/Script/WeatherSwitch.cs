using UnityEngine.Events;
using UnityEngine;

public class WeatherSwitch : MonoBehaviour
{
    public UnityEvent switchWeather;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            switchWeather?.Invoke();
    }
}
