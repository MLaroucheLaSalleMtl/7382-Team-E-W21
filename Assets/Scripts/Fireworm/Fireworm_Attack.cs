using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworm_Attack : MonoBehaviour
{

    //Chris -- ALL CHRIS ;)
    [Header("Projectile GameObject")]
    [Tooltip("Projectile GameObject that will be instantiated when enemy attacks player")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float angle;
    [Tooltip("Head object for which the position of the projectile will be instantiated")]
    [SerializeField] private GameObject head;
    [Tooltip("Max projectiles that can be fired")]
    [Range(1, 5)] [SerializeField] private int maxProj;
    private Animator animator;
    private SpriteRenderer _7up; //Cache the SpriteRenderer of the Fireworm
    [SerializeField] private AudioSource attacksfx;




    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _7up = gameObject.GetComponent<SpriteRenderer>();
        
    }

    // --------------------------- RANGE ATTACK ----------------------------- //

    public void Shoot()
    {
        attacksfx.Play();
        if (animator.GetBool("Dead") != true)//modified
        {
            animator.SetBool("Range", true);
            //GameObject player = GameObject.FindGameObjectWithTag("Player");        
            Vector2 spawn = head.transform.position;
            bool flip = _7up.flipX;
            //Vector3 toDirection = new Vector3(0f,0f,0f);

            for (int i = 0; i < maxProj; i++)
            {
                GameObject temp =
                Instantiate(projectile, spawn, Quaternion.FromToRotation(transform.position, /*new Vector3(0f, 0f, 0f)*/GameObject.FindGameObjectWithTag("Player").transform.position));
                if (i > 0) //For every projectile that is instantiated and that is not the first
                {
                    float inc = (i < 3) ? 1 : 2;
                    //Spread the projectiles             If {i} is even or odd shoot upwards or downwards
                    temp.GetComponent<Rigidbody2D>().AddForce(((i % 2 == 0) ? Vector2.up : Vector2.down) * inc, ForceMode2D.Impulse);
                    //Rotate the projectiles depending if they are going up or down
                    //                    offset inc value to make it reusable
                    temp.transform.Rotate(0f, 0f, ((flip) ? -angle : angle) * (inc - 0.5f) * ((i % 2 == 0) ? 1f : -1f));
                }
            }
        }

    }

    // --------------------------- CHARGE ATTACK ----------------------------- //

    [Header("Charge Attack Properties")]
    [Tooltip("Amount of time spent to charge up before start of attack")]
    [SerializeField] private float ChargeUpTime;
    [Tooltip("Max amount of time that can be spent charging")]
    [Range(0f,1f)] [SerializeField] private float maxChargeTime;
    [Tooltip("Speed that the Fireworm will move when using charge attack")]
    [SerializeField] private float ChargingSpeed;
    [Tooltip("Amount of damage the Charge Attack will deal if it hits the player")]
    [SerializeField] private int ChargeDMG;
    bool charging = false; //Used for OnCollision to allow damaging the player on contact ONLY when charging
    private Rigidbody2D _rb;

    // ------- Method Called to perform actual attack ------- 
    public void Charge()
    {
        Debug.Log("Enemy script disabled.");
        gameObject.GetComponent<Enemy>().enabled = false;
        Debug.Log("Charge Method has been broadcasted, now attempting to Charge");
        blink = true;
        StartCoroutine("Blinking");
        StartCoroutine("ChargingUp");
        //StartCoroutine("StopChargingAfter", maxChargeTime);
        //ChargeTowardsPlayer(TargetPlayer());
    }

    // ------ Coroutine for Delaying movement/attack ------ 
    IEnumerator ChargingUp() //Used as a way to delay the charge attack
    {
        yield return new WaitForSeconds(ChargeUpTime);
        Debug.Log("Charging set to true.");
        charging = true; //Start charging
        blink = false;
        StartCoroutine("StopChargingAfter", maxChargeTime);
    }

    // ------- Coroutine to stop running after a given time when starting to charge  ------------ 
    IEnumerator StopChargingAfter(float t)
    {
        yield return new WaitForSeconds(t);
        Debug.Log("Charging set to false.");
        charging = false;
        gameObject.GetComponent<Enemy>().enabled = true;
    }
    
    // ------ Method for actual displacement in World Space ------ 
    void ChargeTowardsPlayer(Vector2 ppos)
    {
        Debug.Log("ChargingTowardsPlayer NOW!");
        StartRunningAnimation();
        //transform.position = Vector2.MoveTowards(transform.position, ppos, ChargingSpeed * Time.deltaTime);
        Vector2 dir = new Vector2(ppos.x - gameObject.transform.position.x, ppos.y - gameObject.transform.position.y);
        //_rb.AddForce(dir, ForceMode2D.Impulse);
        _rb.velocity = dir*ChargingSpeed;
        
    }

    // -------- Damage Player if we collide with them ---------
    private void OnCollisionEnter2D(Collision2D other)
    {
        bool hitPlayer = other.gameObject.tag == "Player";

        //if(!hitPlayer) {}
        if (hitPlayer && charging) { other.gameObject.BroadcastMessage("PlayerHit", ChargeDMG); }
        charging = false;
    }

    // -------------------------------------------------------------------- //
    // ----- Extra Methods used to organize process of Charge Attack ------ //
    void StartRunningAnimation()
    {
        bool t = true;
        animator.SetBool("Walk", t);
        animator.SetBool("Detected", t);
        animator.SetBool("Attack", !t);
        animator.SetBool("Dead", !t);
        animator.SetBool("Range", !t);
    }

    //Returns Vector2 Position of player to determine which direction the Fireworm should move
    Vector2 TargetPlayer() 
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }


    //Coroutine to make the SpriteRenderer change colors, effectively a blinking effect, used as a visual representation 
    
    public float blinkFreq; //Frequency of blinking
    bool blink;
    IEnumerator Blinking()
    {
        Vector4 normalColor = new Vector4(1f,1f,1f,1f);
        if(blink)
        {
            Debug.Log("Changing colors for charge attack.");   
            Vector4 newColor = new Vector4(1f,0.4f,0.4f,1f);  
            _7up.color = newColor;
            
            yield return new WaitForSeconds(blinkFreq);

            _7up.color = normalColor;
            
            StartCoroutine("Blinking");
        }
        Debug.Log("Reverting back to white color.");
        _7up.color = normalColor;
        yield return new WaitForSeconds(0f);
    }
    // ------------------------------ **[UPDATE METHOD]** ---------------------------------------
    private void Update() 
    {
        if(charging){ChargeTowardsPlayer(TargetPlayer());}
    }
}
