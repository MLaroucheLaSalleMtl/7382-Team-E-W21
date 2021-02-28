using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private GameManager gameManager;
    private Animator animator;
    private Vector3 deadpos;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.PlayerHP = gameManager.PlayerMAXHP; //Start game with max amount of HP possible
        animator = gameObject.GetComponent<Animator>();
    }

    private void PlayerHit(int damage)
    {
        int newDmg = damage;
        if(gameManager.PlayerArmor > 0) //If we have armor, 
        {
            gameManager.PlayerArmor -= damage; //The armor takes damage
            if(gameManager.PlayerArmor < 0) //If the damage we receive is higher than the amount of armor we have,
            {
                newDmg = -(gameManager.PlayerArmor); //The negative value of armor turns into the dmg that HP takes,
                gameManager.PlayerArmor = 0; //And we reset the armor value to 0
            }
        }
        gameManager.PlayerHP -= newDmg; //Take damage to HP
        StartCoroutine("toggleHit");
    }

    IEnumerator toggleHit()
    {
        animator.SetBool("Hit",true);
        yield return new WaitForSeconds(0.01f);
        animator.SetBool("Hit", false);
    }

    private void checkDead()
    {
        if(gameManager.PlayerHP <= 0)
        {   deadpos = gameObject.transform.position;
            gameObject.transform.position = deadpos;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            animator.SetBool("Dead", true);
        }
    }

    private void Dead()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        checkDead();
    }
}
