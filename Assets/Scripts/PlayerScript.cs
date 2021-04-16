using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private GameManager gameManager;
    private Animator animator;
    private Vector3 deadpos;
    private Vector3 _localPos;
    [SerializeField] private GameObject DeadScreen;

    //Ming
    [SerializeField] AudioSource fire,flesh,eletric;

    private void Awake() 
    {
       _localPos = gameObject.transform.localPosition; 
       if(DeadScreen.activeSelf){DeadScreen.SetActive(false);}
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
        else if (gameManager.PlayerArmor <= damage)
        {
            newDmg = damage - gameManager.PlayerArmor;
            gameManager.PlayerArmor = 0;
        }

        gameManager.PlayerHP -= newDmg; //Take damage to HP
        StartCoroutine("toggleHit");
        //Debug.Log($"Player was hit for {newDmg} damage");
    }

    void DmgSound(string DmgType)//play hit sound responding to dmgType
	{
        if(DmgType=="flesh")
        {
            flesh.Play();
        
        }
        else if(DmgType=="fire")
		{
            fire.Play();
		}
        else if (DmgType == "eletric")
        {
            eletric.Play();
        }
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

    public void ShowDeadScreen()
    {
        GameManager.instance.isPaused = true;
        Time.timeScale = 0f;
        DeadScreen.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        checkDead();
        //followParent();
    }
}
