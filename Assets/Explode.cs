using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] float activeTime = 1;
    // Start is called before the first frame update
    Collider2D enemy;
    void Start()
    {
        
    }

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
    
	private void OnTriggerStay2D(Collider2D collision)
	{
        enemy = collision;
        if (collision.tag == "Monster" && enemy == collision)
        {
            //collision.GetComponent<Enemy>().TakeDMG(GameManager.instance.Gadget.Dmg);
            //Debug.Log("Enemy enter explosion area");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Monster"&&enemy==other)
        {
            other.GetComponent<Enemy>().TakeDMG(GameManager.instance.Gadget.Dmg);
            Debug.Log("Enemy enter explosion area");
        }
    }
	private void OnTriggerExit(Collider other)
	{
        if(enemy==other)
		{
            enemy = null;
		}
		
	}
}
