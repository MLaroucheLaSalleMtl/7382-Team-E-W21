using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Item gadget_data;
    public bool de_velocity=false,explode=false;
    //Rigidbody2D rb;
    Vector3 velocity;
    [SerializeField] float deVelocity = 3;
    [SerializeField] Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        gadget_data = this.GetComponent<PickupItem>().itemData;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        gadget_data = this.GetComponent<PickupItem>().itemData;

        if(de_velocity)
		{
            DecreaseVOverTime(deVelocity);
            this.GetComponent<PickupItem>().enabled = false;
            this.gameObject.GetComponent<Animator>().enabled = false;
            //this.gameObject.AddComponent<Rigidbody2D>();
            //this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

        }
        

        velocity = this.gameObject.GetComponent<Rigidbody2D>().velocity;
        //Debug.Log(velocity);
        //Debug.Log("grenade speed:" + this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
        if(explode)
		{
            ExplosionTrigger();
		}
        if(this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude<0.1 && de_velocity == true)
		{
            explode = true;
		}
    }
    void DecreaseVOverTime(float deVelocity)
	{

        //de_velocity = true;
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.drag = deVelocity;
        
        //this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
    }

    // set active
    public void SetDecreasing(bool condition)
	{
        de_velocity = condition;
        //explode = condition;
        //Debug.Log("develocity&explode set to " + condition);
	}
	//explosion efx & dmg
	void ExplosionTrigger()
	{
        //explode = true;
        Instantiate(gadget_data._bulletPpref, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Destroy(this.gameObject);
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Monster" && de_velocity == true)
		{
            if (gadget_data.Code == "Item_G0_Grenade")
            {
                ExplosionTrigger();
            }
            
        }
		
	}
}
