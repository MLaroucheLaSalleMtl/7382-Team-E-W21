using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{

    private GameManager gameManager;
    private Animator animator;
    private Vector3 deadpos;
    private Vector3 _localPos;

    private void Awake() 
    {
       _localPos = gameObject.transform.localPosition; 
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.PlayerArmor = gameManager.PlayerMAXArmor; //Start game with amx amount of Armor possible
        gameManager.PlayerHP = gameManager.PlayerMAXHP; //Start game with max amount of HP possible
        animator = gameObject.GetComponent<Animator>();
    }

    private void PlayerHit(int damage)
    {
        float newDmg = 0;
        
        if(gameManager.PlayerArmor > damage)
        {
            gameManager.PlayerArmor -= damage;
        }
        else if (gameManager.PlayerArmor < damage)
        {
            newDmg = damage - gameManager.PlayerArmor;
            gameManager.PlayerArmor = 0;
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
        {   
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            animator.SetBool("Dead", true);
        }
    }
    
    private void Dead()
    {
        Destroy(this.gameObject);
    }


    private void followParent()
    {
        gameObject.transform.localPosition = _localPos;
    }



    // Update is called once per frame
    void Update()
    {
        checkDead();
        //followParent();
    }
}
