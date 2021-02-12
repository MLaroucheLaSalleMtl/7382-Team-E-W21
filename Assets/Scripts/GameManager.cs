using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Character Stats")] //Used to retrieve player's stat values
    private int playerHP;
    [SerializeField] private int playerMAXHP; //Highest HP the player can have at any one time.
    [Range(0,50)] [SerializeField] private int playerArmor; //Player Armor/tempHP at start of game.
    [Range(1f,300f)] [SerializeField] private int playerSpeed; //The movement speed of the player.
    [Range(0f, 0.5f)] [SerializeField] private float playerCDReduc; //Cooldown Reduction on Dash/Items ---- Range is for testing purposes
    [Range(0f, 0.5f)] [SerializeField] private float playerRSpeed; //Reload Speed of player ---- Range is for testing purposes
    private int playerSP = 3; //Number of stat points player has. ---- "3" hardcoded for testing. !Remove after!
    private int playerLvl; //Level of player

    public int PlayerHP { get => playerHP; set => playerHP = value; }
    public int PlayerMAXHP { get => playerMAXHP; set => playerMAXHP = value; }
    public int PlayerArmor { get => playerArmor; set => playerArmor = value; }
    public int PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }
    public float PlayerCDReduc { get => playerCDReduc; set => playerCDReduc = value; }
    public float PlayerRSpeed { get => playerRSpeed; set => playerRSpeed = value; }
    public int PlayerSP { get => playerSP; set => playerSP = value; }
    public int PlayerLvl { get => playerLvl; set => playerLvl = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
