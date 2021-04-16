using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_is_hit : MonoBehaviour
{
	//public bool ishit=false;
	private float ActiveTime=3f;
	public static Bullet_is_hit instance = null;
	
	//Chris ---- Mods Vars
	int hitcounter = 0; //For piercing bullet, reduces damage according to nbEnmyHt;
	bool piercepewpew = false;
	int wowmuchdmg;
	int reduc = 10;
	float maxFlight = 0.4f;

	IEnumerator flightTime(float t)
	{
		yield return new WaitForSeconds(t);
		Destroy(this.gameObject);
	}

	//Chris -- End of mods Vars

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player"&&other.tag!="Bullet"&&other.tag!="EnemyAttackBox"&&other.tag!="Room"&&other.tag!="Weapon")
		{
			//instantiate pick effect here
			//Destroy(other.gameObject);
			//other.GetComponent<Enemy>().TakeDMG(GameManager.instance.CurrentWeaponData.CurrentWeaponData.Dmg);
			if(GameManager.instance.CurrentWeaponData.Code.Contains("WAR1") /*Chris added parameters*/
				|| GameManager.instance.CurrentWeaponData.Code.Contains("WSR1"))
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
				if(GameManager.instance.CurrentWeaponData.Code.Contains("WS1")) //Charge Pistol
				{
					other.GetComponent<Enemy>().TakeDMG(GameManager.instance.CurrentWeaponData.Dmg+
						10 * Convert.ToInt32(GameManager.instance.scaling));
				}
				//Chris --- Added piercepewpew for pierce weapons
				if(piercepewpew)
				{
				Debug.Log("Yes, I pierced.");
				other.GetComponent<Enemy>().TakeDMG(wowmuchdmg-(hitcounter*reduc));
				++hitcounter;
				}
				//Chris ---- Hitcounter used to reduce damage per pierced enemy
				else
				{
					other.GetComponent<Enemy>().TakeDMG(GameManager.instance.CurrentWeaponData.Dmg);
				}
				
				//GameObject.Find("---GameManager---").GetComponent<GameManager>().AddItem(data_Itemspwan);
				//Debug.Log("Hit monster");

				///Conditions of destory
				if (GameManager.instance.CurrentWeaponData.Code == "Item_WAR1_AR" //Laser
					|| GameManager.instance.CurrentWeaponData.Code == "Item_WSR1_SniperR") //Pierce Sniper
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
		
		// if(other.tag == "Room")
		// {
		// 	Destroy(this.gameObject);
		// }

		//Debug.Log("Trigger");
		//ishit = true;
	}

	//Chris ------ Coroutine and Variables to make laser deal damage every instance INSTEAD of all at once

	bool isRunning = false;
	float dps = 0.1f;
	IEnumerator laserPew(float t)
	{
		isRunning = true;
		yield return new WaitForSeconds(t);
		isRunning = false;
	}



	//Chris ------ End of modifications
	private void OnTriggerStay2D(Collider2D collision)
	{
		//If laser is firing
		if (collision.tag == "Monster"&& GameManager.instance.CurrentWeaponData.Code == "Item_WAR1_AR" 
			/*Chris added parameter*/ && !isRunning)
		{			
			Debug.Log("AR1 doing dmg");			
			collision.GetComponent<Enemy>().TakeDMG(GameManager.instance.CurrentWeaponData.Dmg);
			StartCoroutine("laserPew", dps); //To make laser fire in and out do and do dmg every few instances.
		}
	}
	private void Start()
	{
		//Chris Added these on 04/13/2021
		StartCoroutine("flightTime", maxFlight);
		wowmuchdmg = GameManager.instance.CurrentWeaponData.Dmg;
		if(GameManager.instance.CurrentWeaponData.Code.Contains("WSR1") || GameManager.instance.CurrentWeaponData.Code.Contains("WAR1"))
		{
			piercepewpew = true;
		}
		//End of Chris's Additions
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
