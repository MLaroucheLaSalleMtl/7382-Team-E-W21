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
                /*if (itemData.Code.Contains("Item_C"))
                {
                    Debug.Log("Item_C contains");
                    
                }
                
                else
                {
                    Destroy(gameObject);
                    GameManager.instance.AddItem(itemData);
                }*/
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
            WeaponExchange(collision.gameObject);

            Destroy(this.gameObject);
        }
    }
    //use Item.code to search for specific one, can be opt later.(dont need if condition)
    void WeaponExchange(GameObject player)
    {
        if (itemData.Code == "Item_WAR0_AR")// == "Item_WAR0_AR")
        {
            Debug.Log("Item_WAR0_AR detected");
            Item pre_gun = player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;

            //player.transform.GetComponent<Equipment>().CurrentWeaponData = itemData;
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemData.itemSprite;
            player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data = itemData;
            player.transform.GetChild(0).transform.localScale = itemData._Pref.transform.localScale * 0.1f;
            Debug.Log("current weapon data change to " + player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data);
            //Destroy(this.gameObject);
            Instantiate(pre_gun._Pref, this.gameObject.transform.position + new Vector3(0.5f, 0, 0), pre_gun._Pref.transform.rotation);

        }
        else if (itemData.Code == "Item_WAR1_AR")// == "Item_WAR0_AR")
        {
            Debug.Log("Item_WAR1_AR detected");
            Item pre_gun = player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;

            //player.transform.GetComponent<Equipment>().CurrentWeaponData = itemData;
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemData.itemSprite;
            player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data = itemData;
            player.transform.GetChild(0).transform.localScale = itemData._Pref.transform.localScale * 0.1f;
            Debug.Log("current weapon data change to " + player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data);
            //Destroy(this.gameObject);
            Instantiate(pre_gun._Pref, this.gameObject.transform.position + new Vector3(0.5f, 0, 0), pre_gun._Pref.transform.rotation);

        }
        else if (itemData.Code == "Item_WP0_Pistol")// == "Item_WP0_Pistol")
        {
            Debug.Log("Item_WP0_Pistol detected");
            Item pre_gun = player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;

            //player.transform.GetComponent<Equipment>().CurrentWeaponData = itemData;
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemData.itemSprite;
            player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data = itemData;
            player.transform.GetChild(0).transform.localScale = itemData._Pref.transform.localScale * 0.1f;
            Debug.Log("current weapon data change to " + player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data);

            Instantiate(pre_gun._Pref, this.gameObject.transform.position + new Vector3(0.5f, 0, 0), pre_gun._Pref.transform.rotation);

        }
        else if (itemData.Code == "Item_WP1_Pistol")// == "Item_WP0_Pistol")
        {
            Debug.Log("Item_WP1_Pistol detected");
            Item pre_gun = player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;

            //player.transform.GetComponent<Equipment>().CurrentWeaponData = itemData;
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemData.itemSprite;
            player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data = itemData;
            player.transform.GetChild(0).transform.localScale = itemData._Pref.transform.localScale * 0.1f;
            Debug.Log("current weapon data change to " + player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data);

            Instantiate(pre_gun._Pref, this.gameObject.transform.position + new Vector3(0.5f, 0, 0), pre_gun._Pref.transform.rotation);

        }
        else if (itemData.Code == "Item_WS0_Shotgun") //== "Item_WS0_Shotgun")
        {
            Debug.Log("Item_WS0_Shotgun detected");
            Item pre_gun = player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;

            //player.transform.GetComponent<Equipment>().CurrentWeaponData = itemData;
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemData.itemSprite;
            player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data = itemData;
            player.transform.GetChild(0).transform.localScale = itemData._Pref.transform.localScale * 0.1f;
            Debug.Log("current weapon data change to " + player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data);
            //Destroy(this.gameObject);
            Instantiate(pre_gun._Pref, this.gameObject.transform.position + new Vector3(0.5f, 0, 0), pre_gun._Pref.transform.rotation);

        }
        else if (itemData.Code == "Item_WS1_Shotgun") //== "Item_WS0_Shotgun")
        {
            Debug.Log("Item_WS0_Shotgun detected");
            Item pre_gun = player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;

            //player.transform.GetComponent<Equipment>().CurrentWeaponData = itemData;
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemData.itemSprite;
            player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data = itemData;
            player.transform.GetChild(0).transform.localScale = itemData._Pref.transform.localScale * 0.1f;
            Debug.Log("current weapon data change to " + player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data);
            //Destroy(this.gameObject);
            Instantiate(pre_gun._Pref, this.gameObject.transform.position + new Vector3(0.5f, 0, 0), pre_gun._Pref.transform.rotation);

        }
        else if (itemData.Code == "Item_WSR0_SniperR") //== "Item_WSR0_SniperR") //need to flip the image
        {
            Debug.Log("Item_WSR0_SniperR detected");
            Item pre_gun = player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;

            //player.transform.GetComponent<Equipment>().CurrentWeaponData = itemData;
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemData.itemSprite;
            player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data = itemData;
            player.transform.GetChild(0).transform.localScale = itemData._Pref.transform.localScale * 0.1f;
            //player.transform.GetChild(0).transform.localScale.
            Debug.Log("current weapon data change to " + player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data);
            //Destroy(this.gameObject);
            Instantiate(pre_gun._Pref, this.gameObject.transform.position + new Vector3(0.5f, 0, 0), pre_gun._Pref.transform.rotation);
        }
        else if (itemData.Code == "Item_WSR1_SniperR") //== "Item_WSR0_SniperR") //need to flip the image
        {
            Debug.Log("Item_WSR1_SniperR detected");
            Item pre_gun = player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;

            //player.transform.GetComponent<Equipment>().CurrentWeaponData = itemData;
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemData.itemSprite;
            player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data = itemData;
            player.transform.GetChild(0).transform.localScale = itemData._Pref.transform.localScale * 0.1f;
            //player.transform.GetChild(0).transform.localScale.
            Debug.Log("current weapon data change to " + player.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data);
            //Destroy(this.gameObject);
            Instantiate(pre_gun._Pref, this.gameObject.transform.position + new Vector3(0.5f, 0, 0), pre_gun._Pref.transform.rotation);

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
