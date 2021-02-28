using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Attack : MonoBehaviour
{
    
    [Tooltip("Affected Area that can hit the player")]
    [SerializeField] private GameObject hitbox;
    [Tooltip("How much damage will the skeleton do")]
    [SerializeField] private int damage;    
    private Animator parent;
    
    [SerializeField] private Collider2D inZone;

    private void Start() 
    {
        //hitbox.SetActive(false);
        parent = gameObject.GetComponentInParent<Animator>();
    }
    
    // private void OnTriggerStay2D(Collider2D other) 
    // {
    //     if(other.gameObject.tag == "Player") //If melee hits player
    //     {
    //         other.gameObject.BroadcastMessage("PlayerHit", damage);
    //     }
    //     hitbox.SetActive(false);
    //     Debug.Log($"Colliding with {other.gameObject.tag}");
    // }   

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            inZone = other;
        }
    }

    public void _MeleeHit()
    {
        inZone.gameObject.BroadcastMessage("PlayerHit", damage);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            inZone = null;
        }
    }
}