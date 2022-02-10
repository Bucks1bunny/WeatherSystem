using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeatherData
{
    public string name;
    public ParticleSystem particleSystem;
    public Material skyBox;

    [HideInInspector]
    public ParticleSystem.EmissionModule emission;

    public bool useAudio;
    public AudioClip weatherAudio;
    public float audioVolume;


    public float lightIntensity;
    public float fogChangeSpeed;

    public Color fogColor;
    public Color currentFogColor;
}
