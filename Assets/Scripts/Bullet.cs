using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log($"Colliding with {other.gameObject.name}");
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.BroadcastMessage("TakeDMG", damage);
        }

        if(other.gameObject.tag != "Player" || other.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
        }
    }

}
