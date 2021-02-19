using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    [Header ("Enemy Stat Values")]
    [SerializeField] private int HP;
    [Range (0,200)] [SerializeField] private int Speed;
    [SerializeField] private int ATK;
    [SerializeField] private Animator animator;
    [Header ("Enemy Range Values")]
    [SerializeField] private float detectRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float retreatRange;
    [SerializeField] private float stopRange;
    [Header ("Projectile GameObject")]
    [SerializeField] private GameObject projectile;


    private GameObject player;
    private bool retreat;
    [Range (0f,1f)] [SerializeField] private float retreatReset; //How long can this unit retreat for, adjust to prevent constant retreat
    private float retreatTimer;


    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>(); //Cache animator component
        player = GameObject.FindGameObjectWithTag("Player"); //Cache player Game Object
        retreatReset = 0f;
    }

    private void TakeDMG(int pdmg) //Method called to reduce HP value when hit by player
    {
        this.HP -= pdmg; //Reduce HP by pdmg(player dmg/atk value)
        //if this unit's HP is 0 or below, it is considered dead and triggers the death animation
        if(this.HP <= 0)
        {
            animator.SetBool("Dead", true);
        }
    }

    public void Dead()  //Destroy this object when HP is 0; Called at end of Death Animation
    {
        Destroy(this.gameObject);
    }

    private void detectPlayer() //Method to return whether the player is in range of detection
    {
        //Calculate current distance between this unit and the player
        float distance = Vector2.Distance(transform.position, player.transform.position);
        //If the distance is smaller or equal to its detection range, this unit has thus detected the player
        animator.SetBool("Detected", (distance <= this.detectRange)?true:false);
        
    }

    public void Attack()
    {

        animator.SetBool("Attack", true);
    }

    
    private void Walk() //Change enemy position in scene in accordance to player position
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < stopRange && distance > retreatRange) //Stop moving if in attack range, and not in retreat range
        {
            transform.position = this.transform.position;
            animator.SetBool("Walk", false); //Stop walking animation, will transition to idle animation, then eventually attack animation
            retreat = false;
        }
        else if (distance < retreatRange) //Retreat if player is too close
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -Speed*Time.deltaTime);
            animator.SetBool("Walk",true); //Begin walking animation
            retreat = true;
        }
        else if (distance > attackRange) //Move towards player if not in attack range
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed*Time.deltaTime);
            animator.SetBool("Walk",true); //Begin walking animation
            retreat = false;
        }
    }

    private void FlipSprite()
    {
        SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>(); //Reference to this object's Sprite
        float flip = transform.position.x - player.transform.position.x;
        //Flip sprite image depending if player is on left or right of this unit and if not retreating
        sprite.flipX = ((flip > 0 && retreat == false) || (flip < 0 && retreat == true))?true:false;
    }

    // Update is called once per frame
    void Update()
    {
        //If player is not detected, continue searching for player
        if(!animator.GetBool("Detected")) {detectPlayer();}
        Walk();
        FlipSprite();
        //Reset retreat (to prevent infinite retreating)
        if(retreat == true && retreatTimer < retreatReset)
        {
            retreatTimer += Time.deltaTime;
            if(retreatTimer >= retreatReset)
            {
                retreatTimer = -2f;
                retreat = false;
            }
        }
    }
}
