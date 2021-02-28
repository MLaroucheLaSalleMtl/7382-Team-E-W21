using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworm_Fireball : MonoBehaviour
{    
    private Animator animator;
    private float travelTime;

    [Header("Fireball Values")]
    [Tooltip ("Damage each fireball deals")]
    [SerializeField] private int damage;
    [Tooltip ("Max duration the fireball can stay in flight before automatically exploding")]
    [SerializeField] private float maxAir; 
    [Tooltip ("Speed of which the fireball travels")]
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private SpriteRenderer flip;

    private void Awake() 
    {
        animator = gameObject.GetComponent<Animator>();
        rb  = gameObject.GetComponent<Rigidbody2D>(); //Fireball's rigidbody2D
        flip = gameObject.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float playerx = player.transform.position.x;
        float thisx = transform.position.x;

        rb.AddForce(((playerx<thisx)?Vector2.left:Vector2.right)*speed,ForceMode2D.Impulse); //add force in given direction according to position.x of player
        flip.flipX = (playerx<thisx)?true:false; //flip sprite x value according to position.x of player
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player") //If fireball collides with the player
        {
            rb.velocity = new Vector3(0f,0f,0f);
            other.gameObject.BroadcastMessage("PlayerHit", damage); //Call PlayerHit() Method of player to inflict damage to HP
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
            rb.velocity = new Vector3(0f,0f,0f);
            animator.SetBool("Collided", true); //Begin Explosion animation if projectile is in flight for too long with no collision
        }
    }
}
