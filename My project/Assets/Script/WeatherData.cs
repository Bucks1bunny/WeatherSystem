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
    [Header("Audio")]
    public bool useAudio;
    public AudioClip weatherAudio;
    public float audioVolume;
    [Header("Light")]
    public float lightIntensity;
    [Header("Fog")]
    public float fogDensity;
    public Color fogColor;
}
