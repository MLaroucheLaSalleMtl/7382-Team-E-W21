using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsLevelUp : MonoBehaviour
{
    private GameManager manager;
    [SerializeField] private Text statText;
    
    [SerializeField] private Text spText;
    [Header("Value Increase per stat level")]
    [Range (10,50)] [SerializeField] private int HP;
    [Range(10, 50)] [SerializeField] private int Armor;
    [Range (10,50)] [SerializeField] private int Speed;
    [Range (0.01f, 0.03f)] [SerializeField] private float Cooldown;
    [Range(0.01f, 0.03f)] [SerializeField] private float ReloadSpeed;
    


    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();           
    }

    public void IncreaseStat(string name)
    {        
        if (manager.PlayerSP > 0) //Only perform increase to stat IF the player has stat points to spend.
        {
            switch (name)
            {
                case "incHP":
                    manager.PlayerMAXHP += HP;
                    break;
                case "incArmor":
                    manager.PlayerArmor += Armor;
                    break;
                case "incSpeed":
                    manager.PlayerSpeed += Speed;
                    break;
                case "incCDR":
                    manager.PlayerCDReduc += Cooldown;
                    break;
                case "incRSpeed":
                    manager.PlayerRSpeed += ReloadSpeed;
                    break;
            }

            manager.PlayerSP--;
            ShowStats(); //Used to refresh what is displayed on the stats window, without needing to constantly call in Update()
        }
    }

    public void ShowStats() //Call this method when player presses button to open Stats menu
    {        
        statText.text = "Max HP = " + manager.PlayerMAXHP +
                       "\nArmor = " + manager.PlayerArmor +
                       "\nSpeed = " + manager.PlayerSpeed +
                       "\nCooldown = " + manager.PlayerCDReduc*100 + "%" +
                       "\nReload = " + manager.PlayerRSpeed*100 + "%";

        spText.text = "Skill Points: " + manager.PlayerSP;
        //Disable/enable buttons to increase stats
        GameObject[] toggle = new GameObject[5];
        toggle = GameObject.FindGameObjectsWithTag("StatBtn");
        if (manager.PlayerSP == 0)
        {
            foreach(GameObject o in toggle)
            {
                o.SetActive(false);
            }
        }
        else
        {
            foreach(GameObject o in toggle)
            {
                o.SetActive(true);
            }
        }        
    }
}
