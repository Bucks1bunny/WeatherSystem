using UnityEngine;

public class SandNoise : MonoBehaviour
{
    public Shader snadUpdateShader;
    private Material sandFallMat;
    private MeshRenderer meshRenderer;

    [Range(0.01f, 0.1f)]
    public float flakeAmount;
    [Range(0, 1)]
    public float flakeOpacity;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sandFallMat = new Material(snadUpdateShader);
    }

    // Update is called once per frame
    void Update()
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
