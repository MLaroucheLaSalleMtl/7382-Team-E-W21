using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] Item grenade_data;
    [SerializeField] float activeTime = 1;
    [SerializeField] AudioSource explodsionSfx;
    //Collider2D enemy;
    void Start()
    {
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        explodsionSfx.clip = GameManager.instance.CurrentGagetData._ItemSound0;
        //explodsionSfx.PlayOneShot(grenade_data._ItemSound0);
        explodsionSfx.Play();
    }

    //extra efx
    void Explosion()
    {
        /*if (GameManager.instance.Gadget.Code == "Item_G0_Grenade")
        {

        }*/

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, activeTime);
    }
    
	/*private void OnTriggerStay2D(Collider2D collision)
	{
        enemy = collision;
        if (collision.tag == "Monster" && enemy == collision)
        {
            collision.GetComponent<Enemy>().TakeDMG(GameManager.instance.Gadget.Dmg);
            //Debug.Log("Enemy enter explosion area");
        }
    }*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Monster")
        {
            other.GetComponent<Enemy>().TakeDMG(grenade_data.Dmg);
            Debug.Log("Enemy enter explosion area");
        }
        
    }
	
}
