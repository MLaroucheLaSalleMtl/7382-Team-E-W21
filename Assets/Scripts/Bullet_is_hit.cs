using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_is_hit : MonoBehaviour
{
	//public bool ishit=false;
	private float ActiveTime=3f;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player"&&other.tag!="Bullet")
		{
			//instantisate pick effect here
			//Destroy(other.gameObject);
			//other.GetComponent<Enemy>().TakeDMG(GameObject.Find("PlayerTest").GetComponent<Equipment>().CurrentWeaponData.Dmg);
			if (other.tag == "Monster")
			{

				other.GetComponent<Enemy>().TakeDMG(GameObject.Find("PlayerTest").GetComponent<Equipment>().CurrentWeaponData.Dmg);
				//GameObject.Find("---GameManager---").GetComponent<GameManager>().AddItem(data_Itemspwan);
				//Debug.Log("Hit monster");
			}
			Destroy(gameObject);
			Debug.Log("Hit" + other.name);
			//Debug.Log("Bullet hit");
		}
		
		//Debug.Log("Trigger");
		//ishit = true;
	}
	private void Start()
	{
		Destroy(gameObject, ActiveTime);
	}
	

}
