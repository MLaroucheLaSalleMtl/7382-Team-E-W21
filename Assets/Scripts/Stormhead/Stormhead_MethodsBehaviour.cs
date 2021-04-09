using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stormhead_MethodsBehaviour : MonoBehaviour
{
    [Header ("Stormhead unit values")]
    [Tooltip ("Stormhead Health how much damage can it take before dying")]
    private int Health_Points;
    [Tooltip ("Stormhead movement speed")]
    [Range (0, 10)] [SerializeField] private int speed;
    [Tooltip ("Experience given to player after that the Stormhead dies")]
    [SerializeField] private int experience;
    [Tooltip ("Stormhead Detection range")]
    [Range (0, 20)] [SerializeField] private float detectionRange;
    [Tooltip ("Stormhead Attack range")]
    [Range (0,10)] [SerializeField] private int attackRange;
    private bool canAttack = false;
    
    



    //Cached components and references
    private GameObject p; //Player Game Object
    private float distance; //Distance between player and gameobject
    private Animator animator; //Animator Component
    [SerializeField] private GameObject parentRoom;

    //Attack variables (timer etc.)
    [Header ("Attack variables")]
    [Tooltip ("Delay before following attack")]
    [Range (0f, 2f)] [SerializeField] private float cooldown;
    

    private void Start() 
    {
        p = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private float CheckDistance()
    {
        return Vector2.Distance(transform.position, p.transform.position);
    }
    private void Walk()
    {
        transform.position = Vector2.MoveTowards(transform.position, p.transform.position, speed*Time.deltaTime);
    }

    private void Attack()
    {
        if(!animator.GetBool("AttackCooldown") && !animator.GetBool("Dead"))
        {
            animator.SetBool("Attack", true);
        }  
    }
    
    public void BeginCooldown()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("AttackCooldown", true);
        StartCoroutine("CooldownCoroutine");
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldown);
        animator.SetBool("AttackCooldown", false);
    }    
   
    void Update()
    {
        distance = CheckDistance(); //Update distance between player and gameobject
        animator.SetBool("Detected", distance <= detectionRange); //Update if player is detected or not
        canAttack = distance <= attackRange; //Update if player is in attack range
        animator.SetBool("Walk", !canAttack && animator.GetBool("Detected")); //Start walk animation if player is detected and not in range
        if(animator.GetBool("Detected") && !canAttack){Walk();} //If player is detected but not in attackRange --> Walk()
        if(canAttack){Attack();} //If player can be attacked, begin attack
    }
}
