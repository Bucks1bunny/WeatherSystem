 using UnityEngine.UI;
using UnityEngine;

public class SnowImpact : MonoBehaviour
{
    [SerializeField] private bool isInSnow = false;
    public float slowSpeed;

    private void OnTriggerEnter(Collider other)
    {
        isInSnow = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isInSnow = false;
    }
    private void Update() 
    {
        if (isInSnow)
        {
            if(coldBar.value < coldBar.maxValue)
                coldBar.value += 0.00025f;
        }
        if (!isInSnow)
        {
            if (coldBar.value > coldBar.minValue)
                coldBar.value -= 0.001f;
        }
    }
    private void FixedUpdate()
    {
        if (isInSnow)
            PlayerMovement.playerSpeed = slowSpeed;
        else PlayerMovement.playerSpeed = 4;
    }
}
