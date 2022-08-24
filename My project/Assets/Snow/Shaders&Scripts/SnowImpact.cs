using UnityEngine.UI;
using UnityEngine;

public class SnowImpact : MonoBehaviour
{
    [SerializeField]
    private bool isInSnow = false;
    [SerializeField]
    private PlayerSandTracks sandTracks;
    [SerializeField]
    private PlayerSnowTracks snowTracks;
    [SerializeField]
    private Player player;
    [SerializeField]
    private float slowSpeed;

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
            if (player.statusBar.value < player.statusBar.maxValue)
            {
                player.statusBar.value += 0.0005f;
            }
            if (player.statusBar.value == player.statusBar.maxValue)
            {
                player.TakeDamage(0.1f);
            }
        }
        if (!isInSnow)
        {
            if (player.statusBar.value > player.statusBar.minValue)
            {
                player.statusBar.value -= 0.001f;
            }
        }
    }

    private void FixedUpdate()
    {
        var playerMovement = player.GetComponent<PlayerMovement>();
        if (isInSnow)
        {
            //playerMovement.PlayerSpeed = slowSpeed;
        }
        else
        {
            playerMovement.PlayerSpeed = playerMovement.StartSpeed;
        }
    }
}
