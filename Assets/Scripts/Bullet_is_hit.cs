using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_is_hit : MonoBehaviour
{
	//public bool ishit=false;
	private float ActiveTime=3f;
	public static Bullet_is_hit instance = null;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player"&&other.tag!="Bullet"&&other.tag!="EnemyAttackBox")
		{
			//instantisate pick effect here
			//Destroy(other.gameObject);
			//other.GetComponent<Enemy>().TakeDMG(GameObject.Find("PlayerTest").GetComponent<Equipment>().CurrentWeaponData.Dmg);
			if (other.tag == "Monster")
			{

				other.GetComponent<Enemy>().TakeDMG(GameObject.Find("PlayerTest").GetComponent<Equipment>().CurrentWeaponData.Dmg);
				//GameObject.Find("---GameManager---").GetComponent<GameManager>().AddItem(data_Itemspwan);
				Debug.Log("Hit monster");
				if(GameObject.Find("PlayerTest").GetComponent<Equipment>().CurrentWeaponData.Code != "Item_WAR1_AR"
					|| GameObject.Find("PlayerTest").GetComponent<Equipment>().CurrentWeaponData.Code != "Item_WSR1_SniperR")
				{
					Destroy(gameObject);
				}

			}
			
			//Debug.Log("Hit" + other.name);
			//Debug.Log("Bullet hit");
		}
		//Debug.Log("Trigger");
		//ishit = true;
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Monster"&& GameObject.Find("PlayerTest").GetComponent<Equipment>().CurrentWeaponData.Code == "Item_WAR1_AR")
		{
			Debug.Log("AR1 doing dmg");
			collision.GetComponent<Enemy>().TakeDMG(GameObject.Find("PlayerTest").GetComponent<Equipment>().CurrentWeaponData.Dmg);
		}
	}
	private void Start()
	{
		Item data=GameObject.Find("PlayerTest").GetComponent<Equipment>().CurrentWeaponData;
		if(data.Code.Contains("0")||data.Code== "Item_WSR1_SniperR")
		{
			Destroy(gameObject, ActiveTime);
			//Debug.Log(ActiveTime + " left for bullet just shoot");
		}
		else if(data.Code.Contains("AR1"))
		{
			Debug.Log("new AR's bullet");
			if (instance == null)
			{
				instance = this;
			}
			else if (instance != this)
			{
				Destroy(gameObject);
			}
		}
		
	}
	

}
