using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{   
    public GameObject InventoryMenu;
    //Chris
    public GameObject StatPtsMenu;
    public InputAction StatsWindow;
    public GameObject PauseScreen;
    private Text PauseTxt;
    
    public InputAction PauseBtn;
    //better put it in game manager after intergration GameManager.instance.isPaused

    //Chris
    //Reference to player script to pause any player actions
    private PlayerMovement player;
    // private GunScript gun;
    // private GameObject equipped;

    //---------Bryan Coding----------

    private GameObject BulletInChamber;

    //Ming
    [SerializeField] AudioSource Close,Open;
 
    
    void Start()
    {
        InventoryMenu.gameObject.SetActive(false);
        StatPtsMenu.SetActive(false); //Chris
        StatsWindow.Enable(); //Chris
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Debug.Log("inventory setActive false");
        //Chris
        PauseTxt = PauseScreen.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        OpenInventoryMenu();
        ToggleStatsMenu();
        PauseMenu();
    }

    void OpenInventoryMenu()
    {
        if(Input.GetKeyDown(KeyCode.I)||Input.GetKeyDown(KeyCode.Tab))
        {
            //Debug.Log("Inventory call,"+Input.getkey+  " pressed");
            if(GameManager.instance.isPaused==true)
            {
                Resume();
            }
            else if(GameManager.instance.isPaused==false)
            {
                Pause();
            }
        }
    }
    void CloseInventoryMenu()//close all menu if we gonna have any other UI
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
        else if(Input.GetKeyDown(KeyCode.I)&Time.timeScale == 0f)
        {

        }

    }
    void Resume()
    {
        InventoryMenu.gameObject.SetActive(false);
        //Debug.Log("Menu setactive false");
        Time.timeScale = 1f;
        GameManager.instance.isPaused = false;
        Close.Play();
    }
    void Pause()
    {
        InventoryMenu.gameObject.SetActive(true);
        //Debug.Log("Menu setactive true");
        Time.timeScale = 0f;
        GameManager.instance.isPaused = true;
        Open.Play();
    }

    //Chris
    void ToggleStatsMenu()
    {        
        //If "K" Button is pressed
        if(StatsWindow.triggered)
        {
            //Determine if the window is opened or closed --- Return [true] if active, [false] if not
            bool toggle = StatPtsMenu.activeSelf;
            //If the window is open, setactive to false --- If the window is closed, setactive to true
            StatPtsMenu.SetActive(!toggle);
            //Disable player script when window is opened
            player.enabled = toggle;
            //Pause all GameManager updates if window is opened
            GameManager.instance.isPaused = !toggle;
            //Pause timeScale when window is opened
            Time.timeScale = toggle?1f:0f;         
            
        }
    }    
    
    //Chris
    void PauseMenu()
    {
        //Check if button to open/close the PauseMenu is pressed
        if(Input.GetKeyDown(KeyCode.P) || PauseBtn.triggered)
        {
                bool toggle = PauseScreen.activeSelf;

                player.enabled = toggle;
                GameManager.instance.isPaused = !toggle;
                Time.timeScale = toggle?1f:0f;
                PauseScreen.SetActive(!toggle);        
        }
    }
}
