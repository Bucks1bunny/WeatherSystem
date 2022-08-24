using System;
using UnityEngine;

public class WeatherSwitch : MonoBehaviour
{
    public event Action<int> BiomSwitched = delegate { };

    [SerializeField]
    private Color color;
    [SerializeField]
    private Player player;
    [SerializeField]
    private int Biom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.ChangeFillColor(color);
            BiomSwitched(Biom);
        }
    }
}
