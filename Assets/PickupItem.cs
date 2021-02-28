using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void Start()
    {
        print(gameObject.name);
    }
    //public gameobject EffectwhenPickup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            //instantisate pick effect here
            Destroy(gameObject);
        }
    }
    
}
