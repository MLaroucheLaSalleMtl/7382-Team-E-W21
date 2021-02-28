using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworm_Attack : MonoBehaviour
{

    [Header ("Projectile GameObject")]
    [Tooltip ("Projectile GameObject that will be instantiated when enemy attacks player")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float angle;
    [Tooltip ("Head object for which the position of the projectile will be instantiated")]
    [SerializeField] private GameObject head;
    [Tooltip ("Max projectiles that can be fired")]
    [Range (1, 5)] [SerializeField] private int maxProj;    

    
    void Start()
    {

    }


    public void Shoot()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");        
        Vector2 spawn = head.transform.position;        
        //Vector3 toDirection = new Vector3(0f,0f,0f);

        for(int i = 0; i<maxProj; i++)
        {
            GameObject temp =
            Instantiate(projectile, spawn, Quaternion.FromToRotation(transform.position, new Vector3(0f,0f,0f)));
            if(i > 0) //For every projectile that is instantiated and that is not the first
            {
                float inc = (i < 3)?1:2;
                //Spread the projectiles             If {i} is even or odd shoot upwards or downwards
                temp.GetComponent<Rigidbody2D>().AddForce(((i%2 == 0)?Vector2.up:Vector2.down)*inc, ForceMode2D.Impulse);
                //Rotate the projectiles depending if they are going up or down
                //                    offset inc value to make it reusable
                temp.transform.Rotate(0f,0f,angle*(inc-0.5f)*((i%2 == 0)?1f:-1f));
            }      
        }
    }

    

    private void Update() 
    {
        
    }
    
}
