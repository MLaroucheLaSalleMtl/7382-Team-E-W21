using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stat Values")]
    [Tooltip("Amount of damage this unit can take")]
    [SerializeField] private int HP;
    [Tooltip("Speed that this unit can move")]
    [Range(0f, 10f)] [SerializeField] private float Speed;
    [Tooltip("Amount of damage this unit can deal")]
    // [SerializeField] private int ATK;
    // [Tooltip ("Experience Points this unit will provide upon death")]
    [SerializeField] private int EXP;
    private Animator animator;
    [Header("Enemy Range Values")]
    [Tooltip("The attack method of this enemy, melee or ranged")]
    [SerializeField] private bool melee;
    [Tooltip("Distance between player and enemy before enemy can detect the player")]
    [SerializeField] private float detectRange;
    [Tooltip("Distance between player and enemy before enemy can start attacking")]
    [SerializeField] private float attackRange;
    // [Tooltip ("Distance between player and enemy before the enemy stops moving instead of walking to player")]
    // [SerializeField] private float stopRange;
    [Tooltip("Distance between player and enemy before enemy starts retreating")]
    [SerializeField] private float retreatRange;

    [Header("Enemy reset time")] //Maximum amount of time before this GameObject stops doing specific actions
    [Tooltip("How long can this unit retreat for, adjust to prevent constant retreat")]
    [Range(0f, 1f)] [SerializeField] private float retreatReset; //How long can this unit retreat for, adjust to prevent constant retreat
    [Tooltip("Cooldown before next retreat action can be performed again")]
    [Range(0f, 1f)] [SerializeField] private float retreatCooldown; // Cooldown before next retreat action can be performed again

    [Tooltip("How long can this unit continuously attack for before stopping. \n\nMUST NOT BE LONGER THAN ATTACK \n\t ANIMATION DURATION")]
    [Range(0f, 3f)] [SerializeField] private float attackReset; //How long can this unit attack for before going on attack cooldown
    [Tooltip("Cooldown before next Attack action can be performed again")]
    [Range(0f, 3f)] [SerializeField] private float attackCooldown; // Cooldown before next Attack action can be performed again


    [Header("Attack Properties")]
    [Tooltip("INSTANTIATED PROJECTILE POSITION")]
    [SerializeField] private GameObject attackStart;
    private float headpos;
    [Tooltip("Time before attacking")]
    [SerializeField] private float attackFrame;
    private bool firing;


    private GameObject player; //player GameObject reference
    private float distance; //Distance between the object this script is attached to and the player
    private bool retreat; //bool to tell enemy to retreat(true) or stop retreating(false)

    // -------Timer variables-------
    private float retreatTimer; //timer that goes up for every time.deltaTime spent retreating
    private float retreatCDTimer; //timer for the retreat cooldown, when it reaches same value or higher of retreatCooldown, this enemy can retreat again
    private float attackTimer; //timer that goes up for every time.deltaTime spent attacking
    private float attackCDTimer; //timer for the attack cooldown, when it reaches same value or higher of attackCooldown, this enemy can attack again\




    [Header("Other Properties")]
    [Tooltip("Offset center if pivot is somewhere else (caused by sprite cutting and to maintain consistency of animation")]
    [SerializeField] private Vector3 newCenter;
    [Tooltip("Walking sound audio source")]
    [SerializeField] private AudioSource walkaudio;
    [Tooltip("Audiosource played when this unit dies")]
    [SerializeField] private AudioSource deathaudio;
    public bool Fireworm; //Bool to determine if the game object is the Fireworm (workaround to prevent meddling too much with the script temporary solution)
    private SpriteRenderer sprite;


    // -------ZoneController-------- Bryan
    [SerializeField] private GameObject parentRoom;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>(); //Cache animator component
        player = GameObject.FindGameObjectWithTag("Player"); //Cache player Game Object
        retreatReset = 0f;
        distance = 0f;
        headpos = attackStart.transform.localPosition.x;

        //-----Bryan-------------------------------------------------
        parentRoom.GetComponent<Map>().AddMonster();
        if (Fireworm) { parentRoom.GetComponent<Map>().BossInRoom(true); }
        sprite = gameObject.GetComponent<SpriteRenderer>();
        baseSpeed = Speed;
    }

    public int referHP()
    {
        return HP;
    }

    public void TakeDMG(int pdmg) //Method called to reduce HP value when hit by player
    {
        if (!animator.GetBool("Detected")) { animator.SetBool("Detected", true); }
        this.HP -= pdmg; //Reduce HP by pdmg(player dmg/atk value)
        StartCoroutine("_hit");
        //if this unit's HP is 0 or below, it is considered dead and triggers the death animation
        if (this.HP <= 0)
        {
            animator.SetBool("Dead", true); //Play Death Animation
        }
        if(Speed == 0)
        {
            Speed = baseSpeed;
        }
        Debug.Log($"{gameObject.name} took + {pdmg} dmg");
    }

    Vector4 hitcolor = new Vector4(0.5f,0.5f,0.5f,1);
    Vector4 normcolor = new Vector4(1,1,1,1);
    private float baseSpeed;

    IEnumerator _hit()
    {
        float prevspeed = Speed;
        sprite.color = hitcolor;
        Speed = 0f;
        Debug.Log($"Current speed is set to {Speed}");
        yield return new WaitForSeconds(0.1f);
        sprite.color = normcolor;
        //Speed = baseSpeed;
        Debug.Log($"Current speed is set to {Speed}");
    }
    public void DeathAudio()
    {
        deathaudio.Play();
    }

    public void Dead()  //Destroy this object when HP is 0 and increase player EXP; Called at end of Death Animation
    {
        parentRoom.GetComponent<Map>().ReduceMonster();
        animator.SetBool("Walk", false);
        //animator.SetBool("Dead", false);        
        //Ming
        //int randowm = RandomNumGenerator(0, 5);
        if (Fireworm) { parentRoom.GetComponent<Map>().BossInRoom(false); }
        Debug.Log("it is dead");
        ItemDrop();
    }

    public void RemoveThis()        //Actual method to destroy object -- Put it separate method because doesnt work when inside Dead() for some reason
    {
        GameManager.instance.AddExp(EXP);
        if (Fireworm)
        {
            CapsuleCollider2D cap = gameObject.GetComponent<CapsuleCollider2D>();
            cap.enabled = false;
            Destroy(this);
        }
        else { Destroy(gameObject); }
    }

    //-----------------     Ming       ------------
    int RandomNumGenerator(int from, int to)
    {
        int random = Random.Range(from, to);
        return random;
    }
    public void ItemDrop()
    {
        Debug.Log("drop method runs");
        if (RandomNumGenerator(0, 101) > 95)
        {
            GameObject C0 = Instantiate(GameObject.Find("Item Asset").GetComponent<ItemAssets>().Pill, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
        }
    }
    // ---------------------------------------------
    private void getDistance() //Method to update distance variable, also used to determine whether player has entered the detection range of the enemy
    {
        if (this.gameObject.GetComponent<Animator>().GetBool("Dead") != true)//modified
        {
            //Calculate current distance between this unit and the player
            distance = Vector2.Distance(transform.position, player.transform.position);
            //If the distance is smaller or equal to its detection range, this unit has thus detected the player
            animator.SetBool("Detected", (distance <= this.detectRange) ? true : false);
        }


    }

    private void _AttackTimer()
    {
        if (animator.GetBool("Attack") && attackCDTimer == 0f)
        {
            attackTimer += Time.deltaTime;
            animator.SetBool("AttackCooldown", false);
            if (attackTimer >= attackFrame && firing == false) //Only Shoot when we are Attacking and aren't already firing prevents accidental flamethrower attacks
            {
                if (Fireworm)
                {
                    Debug.Log($"This is a {gameObject.name}.");
                    gameObject.BroadcastMessage(RandomizeFirewormAttack());
                    Debug.Log($"{gameObject.name} is done attacking.");
                }
                firing = true;
            }
        }
        if (attackTimer >= attackReset)
        {
            //if attacking for too long, stop attacking and enter attack cooldown
            attackCDTimer += Time.deltaTime;
            animator.SetBool("Attack", false);
            animator.SetBool("AttackCooldown", true);
        }
        if (attackCDTimer >= attackCooldown) //Reset all parameter variables needed to perform an attack
        {
            attackTimer = attackCDTimer = 0;
            firing = false;
            //Reset all attack timers when we finish coolingdown      
        }
    }

    [Header("Ranged Attack Frequency for Fireworm")]
    [Tooltip("[0 = 0% chance of happening]\n[10 = 100% chance to happen]")]
    [Range(0, 10)] [SerializeField] private int rangeFrequency;

    private string RandomizeFirewormAttack()
    {
        Debug.Log($"Randomizing attack choice of {gameObject.name}.");
        string shoot = "Shoot";
        string charge = "Charge";
        int ran = Random.Range(1, 10);
        Debug.Log($"ran value is {ran}.");
        Debug.Log((ran >= rangeFrequency) ? charge : shoot);
        return (ran >= rangeFrequency) ? charge : shoot;
    }

    private void Walk() //Change enemy position in scene in accordance to player position
    {
        if (animator.GetBool("Detected") && animator.GetBool("Dead") != true)//modified
        {
            if (distance > attackRange && !animator.GetBool("Attack")) //Detected the player and the player is not within attack range, move towards player
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position + newCenter, Speed * Time.deltaTime);
                animator.SetBool("Walk", true);
                retreat = false;
                if (animator.GetBool("Attack")) { animator.SetBool("Attack", false); }
            }
            else if (distance <= attackRange && distance > retreatRange)
            {
                transform.position = this.transform.position; //Stop moving, Start Attacking
                animator.SetBool("Attack", true);
                animator.SetBool("Walk", false);
                retreat = false;
            }
            else if (distance < retreatRange && !animator.GetBool("Attack")) //Retreat
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position + newCenter, -Speed * Time.deltaTime);
                animator.SetBool("Walk", true);
                animator.SetBool("Attack", false);
                retreat = true;
            }
        }
    }

    private void _RetreatTimer()
    {
        if (animator.GetBool("Dead") != true)//modified
        {
            if (retreat == true && retreatCDTimer == 0)
            {
                retreatTimer += Time.deltaTime;
            }
            if (retreat == true && retreatTimer >= retreatReset)
            {
                retreatCDTimer += Time.deltaTime;
                retreat = false;
            }
            if (retreatCDTimer >= retreatCooldown)
            {
                retreatTimer = retreatCDTimer = 0;
            }
        }


    }

    private void FlipSprite()
    {
        if (animator.GetBool("Dead") != true)
        {
            SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>(); //Reference to this object's Sprite
            float flip = transform.position.x - player.transform.position.x;

            //Flip sprite image depending if player is on left or right of this unit and if not retreating
            sprite.flipX = ((flip > 0 && retreat == false) || (flip < 0 && retreat == true)) ? true : false;
            //Flip projectile starting point position to maintain consistency with the sprite
            attackStart.transform.localPosition = new Vector3((flip < 0) ? headpos : -headpos, attackStart.transform.localPosition.y, 0f);
        }


    }


    // Update is called once per frame
    void Update()
    {

        //Update distance between enemy and player to decide what to do next
        getDistance();
        //Enemy continuously tries to detect the player and follow/retreat based on distance range
        Walk();
        FlipSprite();
        //Attack();
        animator.SetBool("Dead", (this.HP <= 0) ? true : false);
        if (walkaudio != null)
        {
            if (animator.GetBool("Walk") && !walkaudio.isPlaying) { walkaudio.Play(); } else if (!animator.GetBool("Walk")) { walkaudio.Pause(); }
        }
        //---------- Reset Timers -------------
        //Reset retreat (to prevent infinite retreating)

        //_RetreatTimer();
        //Reset attack (to prevent infinite attacking)
        _AttackTimer();


    }
}
