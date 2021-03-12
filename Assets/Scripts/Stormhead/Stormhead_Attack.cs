using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stormhead_Attack : MonoBehaviour
{
    [SerializeField] private BoxCollider2D right,left;
    [SerializeField] private int damage;
    private Collider2D inZone;

    
    private void OnTriggerEnter2D(Collider2D other) 
    {        
        if(other.gameObject.tag == "Player")
        {
            inZone = other;
        }   
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            inZone = null;
        }
    }
    public void LightningR()
    {
        if(inZone.gameObject.transform.position.x > transform.position.x)
        inZone.gameObject.BroadcastMessage("PlayerHit", damage);
    }

    public void LightningL()
    {
        if(inZone.gameObject.transform.position.x < transform.position.x)
        inZone.gameObject.BroadcastMessage("PlayerHit", damage);
    }        
    
}
