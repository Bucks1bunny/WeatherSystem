using UnityEngine.Events;
using UnityEngine;

public class WeatherSwitch : MonoBehaviour
{
    public UnityEvent switchWeather;
    public Color color;
    public Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switchWeather?.Invoke();
            player.ChangeFillColor(color);
        }
       
    }
}
