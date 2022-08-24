using UnityEngine;

public class SandNoise : MonoBehaviour
{
    [SerializeField]
    private WeatherSwitch weatherSwitcher;
    [SerializeField]
    private Shader sandUpdateShader;

    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float flakeAmount;
    [SerializeField]
    [Range(0, 1)]
    private float flakeOpacity;

    private Material sandFallMat;
    private MeshRenderer meshRenderer;
    private bool sandBiomReached = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sandFallMat = new Material(sandUpdateShader);
        weatherSwitcher.BiomSwitched += SandBiomReached;
    }

    void Update()
    {
        if (sandBiomReached)
        {
            sandFallMat.SetFloat("_FlakeAmount", flakeAmount);
            sandFallMat.SetFloat("_FlakeOpacity", flakeOpacity);
            RenderTexture sand = (RenderTexture)meshRenderer.material.GetTexture("_Splat");
            RenderTexture temp = RenderTexture.GetTemporary(sand.width, sand.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(sand, temp, sandFallMat);
            Graphics.Blit(temp, sand);
            meshRenderer.material.SetTexture("_Splat", sand);
            RenderTexture.ReleaseTemporary(temp);
        }
    }

    private void SandBiomReached(int biom)
    {
        if(biom == 2)
        {
            sandBiomReached = true;
        }
        else
        {
            sandBiomReached = false;
        }
    }
}
