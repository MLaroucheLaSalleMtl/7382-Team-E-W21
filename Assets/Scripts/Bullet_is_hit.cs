using System;
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
		if (other.tag != "Player"&&other.tag!="Bullet"&&other.tag!="EnemyAttackBox"&&other.tag!="Room")
		{
			//instantiate pick effect here
			//Destroy(other.gameObject);
			//other.GetComponent<Enemy>().TakeDMG(GameManager.instance.CurrentWeaponData.CurrentWeaponData.Dmg);
			if(GameManager.instance.CurrentWeaponData.Code.Contains("WAR1"))
			{

			}
			else
			{
				Destroy(this.gameObject);
			}
			
			//Debug.Log("Hit" + other.name);

			///When hit a enemy, how the bullet beheaves
			if (other.tag == "Monster")
			{
				///Condition of dmg
				if(GameManager.instance.CurrentWeaponData.Code.Contains("WS1"))
				{
					other.GetComponent<Enemy>().TakeDMG(GameManager.instance.CurrentWeaponData.Dmg+
						Convert.ToInt32(GameManager.instance.scaling));
				}
				else
				{
					other.GetComponent<Enemy>().TakeDMG(GameManager.instance.CurrentWeaponData.Dmg);
				}
				
				//GameObject.Find("---GameManager---").GetComponent<GameManager>().AddItem(data_Itemspwan);
				//Debug.Log("Hit monster");

				///Conditions of destory
				if (GameManager.instance.CurrentWeaponData.Code == "Item_WAR1_AR"
					|| GameManager.instance.CurrentWeaponData.Code == "Item_WSR1_SniperR")
				{
					Debug.Log(GameManager.instance.CurrentWeaponData.Code+"'s bullet, don't destory");
				}
				else
				{
					Destroy(this.gameObject);
					Debug.Log(GameManager.instance.CurrentWeaponData.Code+"'s bullet, destory");
				}

			}
		}
		
		//Debug.Log("Trigger");
		//ishit = true;
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Monster"&& GameManager.instance.CurrentWeaponData.Code == "Item_WAR1_AR")
		{
			Debug.Log("AR1 doing dmg");
			collision.GetComponent<Enemy>().TakeDMG(GameManager.instance.CurrentWeaponData.Dmg);
		}
	}
	private void Start()
	{
		
		if(GameManager.instance.CurrentWeaponData.Code.Contains("0"))
		{
			Destroy(gameObject, ActiveTime);
			Debug.Log(ActiveTime + " left for bullet just shoot");
		}
		else if(GameManager.instance.CurrentWeaponData.Code.Contains("AR1"))
		{
			//Debug.Log("new AR's bullet");
			if (instance == null)
			{
				instance = this;
			}
			else if (instance != this)
			{
				Destroy(gameObject);
				Debug.Log("More than one AR1 bullet, destory");
			}
		}
		
	}
	

}
