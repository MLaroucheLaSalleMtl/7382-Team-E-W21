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
        if(collision.tag=="Player")
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
                else if (Input.GetKey(KeyCode.D))
                {
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
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

    }

    //use Item.code to search for specific one, can be opt later.(dont need if condition)
    void WeaponExchange(GameObject player, int goingDir)//4:left, 6:right, 8:up, 2:down
    {
        
        //Debug.Log(itemData.Code + " detected");
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
        else if (goingDir == 4)//going left
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

        //Bryan
        player.GetComponentInChildren<GunScript>().Ammunition = player.GetComponentInChildren<GunScript>().CurrentWeapon_data.MaxAmmo; //Give player the amount of bullets he is supposed to get after picking up new weapon
        player.GetComponentInChildren<GunScript>().TextAmmo.text = player.GetComponentInChildren<GunScript>().CurrentWeapon_data.MaxAmmo + "/" + player.GetComponentInChildren<GunScript>().CurrentWeapon_data.MaxAmmo; 
        //This is for the visual to let the player know how many bullet he has and how many max bullet the gun can have
    }

    void ActivatableExchange()
    {
        if (GameManager.instance.Activatables == null)
        {
            GameManager.instance.Activatables = itemData;
            GameManager.instance.AddItem(itemData);
            Destroy(gameObject);
            
        }
        else if (GameManager.instance.Activatables == itemData)
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
        if (GameManager.instance.CurrentGagetData == null)
        {
            GameManager.instance.CurrentGagetData = itemData;
            GameManager.instance.AddItem(itemData);
            Destroy(gameObject);
            //this.gameObject.GetComponent<Grenade>().SetDecreasing(false);
        }
        else if (itemData == GameManager.instance.CurrentGagetData)
        {
            GameManager.instance.AddItem(itemData);
            //this.gameObject.GetComponent<Grenade>().SetDecreasing(false);
            Destroy(this.gameObject);
        }
        else
        {
            //Debug.Log("Item_WAR0_AR detected");
            //Store player activatable
            Item pre_activatable = GameManager.instance.CurrentGagetData;
            //Player activatable->item
            GameManager.instance.CurrentGagetData = itemData;
            //Player "drop" preb activatable
            itemData = pre_activatable;

            this.gameObject.GetComponent<SpriteRenderer>().sprite = pre_activatable.itemSprite;
            this.gameObject.name = itemData.Code;
            this.gameObject.transform.localScale = pre_activatable._Pref.transform.localScale;
            //this.gameObject.GetComponent<Grenade>().SetDecreasing(false);
            //Debug.Log("pre item localScale: " + pre_activatable._Pref.transform.localScale);
        }
    }

    //Chris (START)
    // ------------- FLOATING EFFECT FOR ITEMS ON GROUND ------------------------
    [Tooltip("How much distance will be traveled at a time, can also be used to control smoothness")]
    [SerializeField] private float float_distance;
    [Tooltip("Amount of distance allowed to travel")]
    [Range(0f, 2f)] [SerializeField] private float maxDistance;
    private float topHeight;
    [Tooltip("Controls speed and smoothness of change of position, smaller values are smoother while higher values are closer to frame by frame")]
    private float refreshTimer = 0.05f;
    //public bool on_floor = true;
    private bool routinerunning;
    private Vector3 startPos;

    private bool limitReached = false;

    private void Start()
    {
        startPos = transform.localPosition; //Cache initial position of item
        topHeight = maxDistance + startPos.y;
    }    
    IEnumerator Floating()
    {
        checkLimitReached(); //Check if we reached Top/Bottom
        routinerunning = true; //Stop CheckOnFloor() from Starting Coroutine again

        //Change position of object
        transform.localPosition = new Vector2(transform.localPosition.x,
                                        transform.localPosition.y + float_distance);

        //Refresh Rate of Coroutine, used to determine how much time to wait before continuing to move the object
        yield return new WaitForSeconds(refreshTimer);

        routinerunning = false; //Allow Coroutine to be called again
    }

    void checkLimitReached()
    {
        //Cache current localPosition.y
        float yPos = transform.localPosition.y;

        //Check if yPos reached the top or bottom and return as a bool
        limitReached = (yPos >= topHeight || yPos < startPos.y);

        //if we reach top or bottom we reverse the floating direction
        if(limitReached){float_distance = -float_distance; limitReached = false;}
    }
    
    private void Update()
    {
        if(!routinerunning){StartCoroutine("Floating");}
    }
    //Chris (END)
}
