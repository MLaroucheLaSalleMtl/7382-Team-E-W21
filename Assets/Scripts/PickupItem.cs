using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    //public GameObject pf_Itemspawn;
    //public GameObject pf_weaponSpwan;
    public Item itemData;
    private void Start()
    {
        
        //print(gameObject.name);
    }
    //public gameobject EffectwhenPickup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            if(this.gameObject.tag=="Item"&& itemData.itemName == "Red Pill"&&GameManager.instance.playerHP<GameManager.instance.PlayerMAXHP)
			{
                GameManager.instance.useItem(itemData);
                Destroy(gameObject);
                
			}
            else if (this.gameObject.tag == "Item"&&itemData.itemType!=Item.ItemType.Pill)
			{
                if (itemData.itemType == Item.ItemType.Syringe)
                {
                    ActivatableExchange();
                }
                else if((itemData.itemType == Item.ItemType.Gaget))
				{
                    GadgetExchange();
				}
				else
				{
                    Destroy(gameObject);
                    GameManager.instance.AddItem(itemData);
                }
                
            }
            else if(this.gameObject.tag=="Weapon")
			{
                Debug.Log("Weapon pick up");
                WeaponExchange(collision.gameObject);
            }
            

            //instantisate pick effect here

                //Debug.Log("Player in contact");
        }
        //Debug.Log("Something in contact");
    }
	
    
	void WeaponExchange(GameObject player)
	{
        if (itemData.Code== "Item_WAR0_AR")// == "Item_WAR0_AR")
        {
            Debug.Log("Item_WAR0_AR detected");
            //store original item data
            Item pre_gun = player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;
            //current weapon data&sprite=this gameObject
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
            player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data = itemData;
            //exchange take place, this gameObject data&sprite = pre weapon
            Debug.Log("pre gun: " + pre_gun);
            itemData = pre_gun;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pre_gun.itemSprite;
            this.gameObject.name = itemData.Code;
        }
        else if (itemData.Code == "Item_WP0_Pistol")// == "Item_WP0_Pistol")
        {
            Debug.Log("Item_WP0_Pistol detected");
            //store original item data
            Item pre_gun = player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;
            //current weapon data&sprite=this gameObject
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
            player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data = itemData;
            //exchange take place, this gameObject data&sprite = pre weapon
            Debug.Log("pre gun: " + pre_gun);
            itemData = pre_gun;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pre_gun.itemSprite;
            this.gameObject.name = itemData.Code;

        }
        else if (itemData.Code == "Item_WS0_Shotgun") //== "Item_WS0_Shotgun")
        {
            Debug.Log("Item_WS0_Shotgun detected");
            //store original item data
            Item pre_gun =player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;
            //current weapon data&sprite=this gameObject
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
            player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data = itemData;
            //exchange take place, this gameObject data&sprite = pre weapon
            Debug.Log("pre gun: " + pre_gun);
            itemData = pre_gun;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pre_gun.itemSprite;
            this.gameObject.name = itemData.Code;
            
        }
        else if (itemData.Code == "Item_WSR0_SniperR") //== "Item_WSR0_SniperR") //need to flip the image
        {
            Debug.Log("Item_WSR0_SniperR detected");
            //store original item data
            Item pre_gun = player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;
            //current weapon data&sprite=this gameObject
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
            player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data = itemData;
            //exchange take place, this gameObject data&sprite = pre weapon
            Debug.Log("pre gun: " + pre_gun);
            itemData = pre_gun;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pre_gun.itemSprite;
            this.gameObject.name = itemData.Code;

        }

        ///this part is covered by additem(), no longer needed
        /*if(this.gameObject.name.Contains("Item_C0_RedPill"))//== "Item_C0_RedPill")
		{
            itemData = GameObject.Find("Item Asset").GetComponent<ItemAssets>().C0;
            GameObject.Find("GameManager").GetComponent<GameManager>().AddItem(itemData);
            Destroy(this.gameObject);
            Debug.Log("found");
        }
        else if (this.gameObject.name.Contains("Item_C1_Seringe"))// == "Item_C1_Seringe")
        {
            itemData = GameObject.Find("Item Asset").GetComponent<ItemAssets>().C1;
            GameObject.Find("GameManager").GetComponent<GameManager>().AddItem(itemData);
            Destroy(this.gameObject);
            Debug.Log("found");
        }
        else if (this.gameObject.name.Contains("Item_G0_Grenade"))// == "Item_G0_Grenade")
        {
            itemData = GameObject.Find("Item Asset").GetComponent<ItemAssets>().G0;
            GameObject.Find("GameManager").GetComponent<GameManager>().AddItem(itemData);
            Destroy(this.gameObject);
            Debug.Log("found");
        }*/
    }
    void ActivatableExchange()
	{
        if (GameManager.instance.Activatables == null)
        {
            GameManager.instance.Activatables = itemData;
            GameManager.instance.AddItem(itemData);
            Destroy(gameObject);
        }
        else
		{
            //Debug.Log("Item_WAR0_AR detected");
            //Store player activatable
            Item preb_activatable = GameManager.instance.Activatables;
            //Player activatable->item
            GameManager.instance.Activatables = itemData;
            //Player "drop" preb activatable
            itemData = preb_activatable;

            this.gameObject.GetComponent<SpriteRenderer>().sprite = preb_activatable.itemSprite;
            this.gameObject.name = itemData.Code;
        }
    }
    void GadgetExchange()
	{
        if(GameManager.instance.Gadget==null)
		{
            GameManager.instance.Gadget = itemData;
            GameManager.instance.AddItem(itemData);
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("Item_WAR0_AR detected");
            //Store player activatable
            Item preb_activatable = GameManager.instance.Gadget;
            //Player activatable->item
            GameManager.instance.Gadget = itemData;
            //Player "drop" preb activatable
            itemData = preb_activatable;

            this.gameObject.GetComponent<SpriteRenderer>().sprite = preb_activatable.itemSprite;
            this.gameObject.name = itemData.Code;
        }
    }
}
