using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseVolumeTrigger : MonoBehaviour
{
    public WeatherSystem weatherSystem;

    [SerializeField] private float outsideVolume;
    [SerializeField] private float spread;
    [SerializeField] private bool inside_outside;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InHouseManager.isInside = inside_outside;
        }
    }
    private void Update()
    {
        if (InHouseManager.isInside)
        {
            if (weatherSystem.audioSource.spatialBlend < outsideVolume) { weatherSystem.audioSource.spatialBlend += Time.deltaTime * 1; }
            if (weatherSystem.audioSource.spread < spread) { weatherSystem.audioSource.spread += Time.deltaTime * 200; }
        }
        else
        {
            if (weatherSystem.audioSource.spatialBlend > outsideVolume) { weatherSystem.audioSource.spatialBlend -= Time.deltaTime * 1; }
            if (weatherSystem.audioSource.spread > spread) { weatherSystem.audioSource.spread -= Time.deltaTime * 200; }

        }
    }
}
