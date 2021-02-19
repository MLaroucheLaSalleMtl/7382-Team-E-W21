using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworm_Fireball : MonoBehaviour
{    
    private Animator animator;
    private float travelTime;

    [Header("Fireball Values")]
    [SerializeField] private int damage;
    [SerializeField] private float maxAir; 
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player") //If fireball collides with the player
        {
            BroadcastMessage("PlayerHit", damage); //Call Hit() Method of player to inflict damage to HP
            animator.SetBool("Collided", true); //Begin Explosion animation
        }
    }

    public void Destroy() 
    {
        Destroy(this.gameObject); //Method used at the end of the explosion animation to destroy the fireball object
    }
    // Update is called once per frame
    void Update()
    {
        travelTime += Time.deltaTime;
        if (travelTime >= maxAir)
        {
            animator.SetBool("Collided", true); //Begin Explosion animation
        }
    }
}
