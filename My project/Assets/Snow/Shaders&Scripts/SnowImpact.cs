using UnityEngine.UI;
using UnityEngine;

public class SnowImpact : MonoBehaviour
{
    [SerializeField] private bool isInSnow = false;
    public PlayerSandTracks sandTracks;
    public PlayerSnowTracks snowTracks;
    public Player player;
    public float slowSpeed;
    private void OnTriggerEnter(Collider other)
    {
        isInSnow = true;
        sandTracks.enabled = false;
        snowTracks.enabled = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        isInSnow = false;
        sandTracks.enabled = true;
        snowTracks.enabled = false;
    }
    private void Update() 
    {
        if (isInSnow)
        {
            if(player.statusBar.value < player.statusBar.maxValue)
                player.statusBar.value += 0.0005f;
            if (player.statusBar.value == player.statusBar.maxValue)
            {
                player.TakeDamage(0.1f);
            }
        }
        if (!isInSnow)
        {
            if (player.statusBar.value > player.statusBar.minValue)
                player.statusBar.value -= 0.001f;
        }
    }
    private void FixedUpdate()
    {
        if (isInSnow)
            PlayerMovement.playerSpeed = slowSpeed;
        else PlayerMovement.playerSpeed = 4;
    }
}
