using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Character Stats")] //Used to retrieve player's stat values

    public float playerHP;
    [SerializeField] private float playerMAXHP; //Highest HP the player can have at any one time.
    [SerializeField] private float playerShield; //Player Armor/tempHP at start of game.
    [SerializeField] private float expToLevel; //Amount of exp needed to level up
    [Range(0f,10f)] [SerializeField] private float playerSpeed; //The movement speed of the player.
    [Range (0f,50f)] [SerializeField] private float playerMAXShield;
    [Range(0f, 0.5f)] [SerializeField] private float playerCDReduc; //Cooldown Reduction on Dash/Items ---- Range is for testing purposes
    [Range(0f, 0.5f)] [SerializeField] private float playerRSpeed; //Reload Speed of player ---- Range is for testing purposes
    private float playerEXP; //Experience Points of player

    private int playerSP; //Number of stat points player has.
    private int playerLvl; //Level of player
    
    //HP,SHIELD,EXP BAR OBJECTS
    [SerializeField] private Image EXP_fill;
    [SerializeField] private Image HP_fill;
    [SerializeField] private Image Shield_fill;

    [SerializeField] private Slider HPBar;
    [SerializeField] private Slider ShieldBar;
    [SerializeField] private Slider EXPBar;

    [SerializeField] private Text HP_Text;

    //Sorting order variables
    [SerializeField] private int sortingOrderBase = 5000;
    [SerializeField] private int scalingOrder = 100;
    [SerializeField] private int offset = 0;
    //---Ming
    public bool isPaused = false;

    public List<Item> items = new List<Item>();
    public List<int> itemNumbers = new List<int>();

    public GameObject[] slots;
    public GameObject bullet_prefab;

    public Item Gadget=null, Activatables=null;
    //---
    //Encapsulation
    public float PlayerHP { get => playerHP; set => playerHP = value; }
    public float PlayerMAXHP { get => playerMAXHP; set => playerMAXHP = value; }
    public float PlayerArmor { get => playerShield; set => playerShield = value; }
    public float PlayerMAXArmor { get => playerMAXShield; set => playerMAXShield = value; }
    public float PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }
    public float PlayerCDReduc { get => playerCDReduc; set => playerCDReduc = value; }
    public float PlayerRSpeed { get => playerRSpeed; set => playerRSpeed = value; }
    public int PlayerSP { get => playerSP; set => playerSP = value; }
    public int PlayerLvl { get => playerLvl; set => playerLvl = value; }
    public int SortingOrderBase {get => sortingOrderBase;}
    public int ScalingOrder {get => scalingOrder;}
    public int Offset {get => offset;}



    private void Awake() //Singleton to assure that we only have one instance of GameManager
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
	private void Start()
	{
        DisplayItem();
        //
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemType==Item.ItemType.Gaget)
            {
                Gadget = items[i];
            }
            else if (items[i].itemType == Item.ItemType.Activatable)
            {
                Activatables = items[i];
            }
        }
    }

	public void SetWeapon(){}

    private void CheckExp()
    {
        if(playerEXP >= expToLevel)
        {
            float temp = playerEXP - expToLevel;
            playerEXP = temp;
            playerLvl++;
            playerSP++;
        }
    }

    public void AddExp(int gains)
    {
        this.playerEXP += gains;
    }
    ///---Ming
    void DisplayItem()
    {
        
        for (int i = 0; i < slots.Length; i++)
        {
            if(i<items.Count)
			{
                //transp decrease, show item image
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemSprite;
                //Debug.Log("image name: " + slots[i].transform.GetChild(0).GetComponent<Image>().sprite.name);
                //transp decrease, show item number
                slots[i].transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, 1);
                slots[i].transform.GetChild(1).GetComponent<Text>().text = itemNumbers[i].ToString();
                //discord bottom show
                slots[i].transform.GetChild(2).gameObject.SetActive(true);
                //Debug.Log("item count:" + items.Count+". first Contains"+items.Contains(items[1]));
            }
			else
			{
                //transp decrease, show item image
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                //Debug.Log("image name: " + slots[i].transform.GetChild(0).GetComponent<Image>().sprite.name);
                //transp decrease, show item number
                slots[i].transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, 0);
                slots[i].transform.GetChild(1).GetComponent<Text>().text = null;
                //discord bottom show
                slots[i].transform.GetChild(2).gameObject.SetActive(false);
                //Debug.Log("item count:" + items.Count+". first Contains"+items.Contains(items[1]));
            }
        }
    }
    public void AddItem(Item item)
    {
        //Debug.Log("Add item(" + item.itemName + ")");
        if (!items.Contains(item))
        {
            items.Add(item);
            itemNumbers.Add(1);
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (item == items[i])
                {
                    itemNumbers[i]++;
                }
            }
        }
        DisplayItem();
    }
    public void RemoveItem(Item item)
	{
        if(items.Contains(item))//if player's inventory have this item already 
		{
            for(int i=0;i<items.Count;i++)
			{
                
                if (item == items[i])
				{
                    itemNumbers[i]--;
                    if(itemNumbers[i]==0)
					{
                        if(Activatables ==item)
						{
                            Activatables = null;
						}
                        items.Remove(item);
                        itemNumbers.Remove(itemNumbers[i]);
					}
                }
            }
		}
		else
		{
            Debug.Log("No such an item found in inventory: " + item);
		}
        //refresh inventory
        DisplayItem();
	}
    public void useItem(Item _item)
    {
        int num;
        for (int i = 0; i < items.Count; i++)
        {
            if (_item == items[i])
            {
                num = i;
                if (_item.Code.Contains("C1") && itemNumbers[num] > 0)//status boost
                {
                    Debug.Log(_item.itemName + " is used.");
                    playerMAXHP += _item.effect_value;
                    RemoveItem(_item);
                }
                else if(_item.Code.Contains("C2") && itemNumbers[num] > 0)
				{
                    Debug.Log(_item.itemName + " is used.");
                    playerSpeed += _item.effect_value*playerSpeed;
                    RemoveItem(_item);
                }
                else if (_item.Code.Contains("C3") && itemNumbers[num] > 0)
                {
                    Debug.Log(_item.itemName + " is used.");
                    playerShield += _item.effect_value;
                    RemoveItem(_item);
                }
            }
			else if (_item.itemType == Item.ItemType.Pill)
            {
                playerHP += _item.effect_value;
                Debug.Log("Player hp: " + playerHP + "---" + _item.itemName + " is used.");
                //Debug.Log("Player hp: " + playerHP);

            }
        }
        

    }
    
    /// </summary>
    void UpdateBars()
    {
        float Hp = PlayerHP/PlayerMAXHP;
        float Shield = PlayerArmor/PlayerMAXArmor;
        float EXP = playerEXP/expToLevel;
        
        HPBar.value = Hp; //Update HP Bar displayed value
        HP_Text.text = $"{PlayerHP}/{PlayerMAXHP}"; //Update HP Bar Text displayed text
        ShieldBar.value = Shield; //Update Shield Bar displayed value
        EXPBar.value = EXP; //Update EXP Bar displayed value
        //Debug.Log($"Player HP Ratio is {Hp}\nPlayer Armor ratio is {Shield}");
        
        HP_fill.enabled = HPBar.value > 0;
        Shield_fill.enabled = ShieldBar.value > 0;
        EXP_fill.enabled = EXPBar.value > 0;

    }
    private void Update() 
    {
        UpdateBars();
        if(Input.GetKeyDown(KeyCode.E)&&Activatables!=null)
		{
            useItem(Activatables);
		}
    }

}
