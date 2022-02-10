using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainImpact : MonoBehaviour
{
   
    [SerializeField] private bool inRainTrigger = false;
    public float force;
    public Vector3 direction;
    public Vector3 oppositeDirection;
    public Rigidbody player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            inRainTrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            inRainTrigger = false;
    }
    private void FixedUpdate()
    {
        if (inRainTrigger)
            player.AddForce(direction * force);
        //StartCoroutine(DirectionChange());
    }
    
   /* IEnumerator DirectionChange()
    {
        player.AddForce(direction * force);
        yield return new WaitForSeconds(1);
        player.AddForce(oppositeDirection * force);
        yield return new WaitForSeconds(1);
    }*/
}
