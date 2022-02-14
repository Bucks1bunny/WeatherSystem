using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnowTracks : MonoBehaviour
{
    private RenderTexture splatmap;
    private Material drawMaterial;
    private Material snowMat;

    public Shader drawShader;
    public GameObject snowTerrain;
    public Transform player;
    RaycastHit groundHit;
    int layerMask;

    [Range(0, 5)]
    public float brushSize;
    [Range(0, 1)]
    public float brushStrength;
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");

        drawMaterial = new Material(drawShader);

        snowMat = snowTerrain.GetComponent<MeshRenderer>().material;
        snowMat.SetTexture("_Splat", splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat));
    }

    void Update()
    {
        if(Physics.Raycast(player.position, -Vector3.up, out groundHit, 1f, layerMask))
        {
            drawMaterial.SetVector("_Coordinate", new Vector4(groundHit.textureCoord.x, groundHit.textureCoord.y, 0, 0));
            drawMaterial.SetFloat("_Strength", brushStrength);
            drawMaterial.SetFloat("_Size", brushSize);
            RenderTexture temp = RenderTexture.GetTemporary(splatmap.width, splatmap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(splatmap, temp);
            Graphics.Blit(temp, splatmap, drawMaterial);
            RenderTexture.ReleaseTemporary(temp);
        }
    }
}
