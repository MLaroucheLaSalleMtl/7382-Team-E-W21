using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Character Stats")] //Used to retrieve player's stat values
    private int playerHP;
    [SerializeField] private int playerMAXHP; //Highest HP the player can have at any one time.
    [Range(0,50)] [SerializeField] private int playerArmor; //Player Armor/tempHP at start of game.
    [Range(0f,5f)] [SerializeField] private int playerSpeed; //The movement speed of the player.
    [Range(0f, 0.5f)] [SerializeField] private float playerCDReduc; //Cooldown Reduction on Dash/Items ---- Range is for testing purposes
    [Range(0f, 0.5f)] [SerializeField] private float playerRSpeed; //Reload Speed of player ---- Range is for testing purposes
    private int playerSP; //Number of stat points player has. ---- "3" hardcoded for testing. !Remove after!
    private int playerLvl; //Level of player
    private int playerEXP; //Experience Points of player

    //Sorting order variables
    [SerializeField] private int sortingOrderBase = 5000;
    [SerializeField] private int scalingOrder = 100;
    [SerializeField] private int offset = 0;
    //---Ming
    public bool isPaused = false;
    public List<Item> items = new List<Item>();
    public List<int> itemNumbers = new List<int>();
    public GameObject[] slots;
    //---
    //Encapsulation
    public int PlayerHP { get => playerHP; set => playerHP = value; }
    public int PlayerMAXHP { get => playerMAXHP; set => playerMAXHP = value; }
    public int PlayerArmor { get => playerArmor; set => playerArmor = value; }
    public int PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }
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

    public void AddExp(int gains)
    {
        this.playerEXP += gains;
    }
    //---Ming
    void DisplayItem()
    {
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemSprite;
            slots[i].transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, 1);
            slots[i].transform.GetChild(1).GetComponent<Text>().text = itemNumbers[i].ToString();

            slots[i].transform.GetChild(2).gameObject.SetActive(true);
            //Debug.Log("item count:" + items.Count+". first Contains"+items.Contains(items[1]));
        }
    }
    public void AddItem(Item item)
    {
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
    //---
}
