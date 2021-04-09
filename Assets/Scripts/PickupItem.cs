using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    //public GameObject pf_Itemspawn;
    //public GameObject pf_weaponSpwan;
    public Item itemData;
    //public gameobject EffectwhenPickup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (itemData.itemType == Item.ItemType.Pill && GameManager.instance.playerHP < GameManager.instance.PlayerMAXHP)//this.gameObject.tag=="Item"&& 
            {
                GameManager.instance.useItem(itemData);
                Destroy(gameObject);

            }
            else if (itemData.itemType == Item.ItemType.Activatable)//this.gameObject.tag == "Item"&&
            {
                Debug.Log("activatable detected");
                ActivatableExchange();
            }
            else if (itemData.itemType == Item.ItemType.Gaget)
            {
                GadgetExchange();
            }

            //instantisate pick effect here

            //Debug.Log("Player in contact");
        }
        //Debug.Log("Something in contact");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (itemData.itemType == Item.ItemType.Weapon)//this.gameObject.tag=="Weapon"
        {
            //Debug.Log("Weapon pick up");
            int goingDir;
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    goingDir = 4;
                    WeaponExchange(collision.gameObject, goingDir);
                    Destroy(this.gameObject);
                }
				else
				{
                    goingDir = 4;
                    WeaponExchange(collision.gameObject, goingDir);
                    Destroy(this.gameObject);
                }
            }
            else if(Input.GetKey(KeyCode.D))
			{
                if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S))
				{
                    goingDir = 6;
                    WeaponExchange(collision.gameObject, goingDir);
                    Destroy(this.gameObject);
				}
                else
                {
                    goingDir = 6;
                    WeaponExchange(collision.gameObject, goingDir);
                    Destroy(this.gameObject);
                }
            }
            else if (Input.GetKey(KeyCode.W))
            {
                goingDir = 8;
                WeaponExchange(collision.gameObject, goingDir);
                Destroy(this.gameObject);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                goingDir = 2;
                WeaponExchange(collision.gameObject, goingDir);
                Destroy(this.gameObject);
            }
        }
        
    }

    //use Item.code to search for specific one, can be opt later.(dont need if condition)
    void WeaponExchange(GameObject player,int goingDir)//4:left, 6:right, 8:up, 2:down
    {
        
        Debug.Log(itemData.Code+" detected");
        Item pre_gun = player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;

        //player.transform.GetComponent<Equipment>().CurrentWeaponData = itemData;
        player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemData.itemSprite;
        player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data = itemData;
        player.transform.GetChild(0).transform.localScale = itemData._Pref.transform.localScale * 0.2f;
        Debug.Log("current weapon data change to " + player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data);
        //Destroy(this.gameObject);

        if (goingDir == 6)//going right
        {
            Instantiate(pre_gun._Pref, this.gameObject.transform.position + new Vector3(-0.5f, 0, 0), pre_gun._Pref.transform.rotation);
        }
        else if(goingDir == 4)//going left
		{
            Instantiate(pre_gun._Pref, this.gameObject.transform.position + new Vector3(0.5f, 0, 0), pre_gun._Pref.transform.rotation);
        }
        else if (goingDir == 8)//going up
        {
            Instantiate(pre_gun._Pref, this.gameObject.transform.position + new Vector3(0, -0.5f, 0), pre_gun._Pref.transform.rotation);
        }
        else if (goingDir == 2)//going down
        {
            Instantiate(pre_gun._Pref, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), pre_gun._Pref.transform.rotation);
        }

    }

    void ActivatableExchange()
    {
        if (GameManager.instance.Activatables == null)
        {
            GameManager.instance.Activatables = itemData;
            GameManager.instance.AddItem(itemData);
            Destroy(gameObject);
        }
        else if(GameManager.instance.Activatables==itemData)
		{
            GameManager.instance.AddItem(itemData);
            Destroy(this.gameObject);
		}
        else
        {
            Debug.Log("activatable!=null");
            //Store player activatable
            Item pre_activatable = GameManager.instance.Activatables;
            //Player activatable->item, additem()
            GameManager.instance.AddItem(itemData);
            GameManager.instance.Activatables = itemData;
            //Player "drop" preb activatable
            itemData = pre_activatable;
            GameManager.instance.RemoveItem(pre_activatable);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pre_activatable.itemSprite;
            this.gameObject.name = itemData.Code;
            this.gameObject.transform.localScale = pre_activatable._Pref.transform.localScale;
            Debug.Log("pre item localScale: " + pre_activatable._Pref.transform.localScale);
        }
    }
    void GadgetExchange()
    {
        if (GameManager.instance.Gadget == null)
        {
            GameManager.instance.Gadget = itemData;
            GameManager.instance.AddItem(itemData);
            Destroy(gameObject);
        }
        else if(itemData==GameManager.instance.Gadget)
		{
            GameManager.instance.AddItem(itemData);
            Destroy(this.gameObject);
		}
        else
        {
            //Debug.Log("Item_WAR0_AR detected");
            //Store player activatable
            Item pre_activatable = GameManager.instance.Gadget;
            //Player activatable->item
            GameManager.instance.Gadget = itemData;
            //Player "drop" preb activatable
            itemData = pre_activatable;

            this.gameObject.GetComponent<SpriteRenderer>().sprite = pre_activatable.itemSprite;
            this.gameObject.name = itemData.Code;
            this.gameObject.transform.localScale = pre_activatable._Pref.transform.localScale;
            //Debug.Log("pre item localScale: " + pre_activatable._Pref.transform.localScale);
        }
    }

}
