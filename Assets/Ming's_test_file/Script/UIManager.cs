using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject InventoryMenu;
    //Chris
    public GameObject StatPtsMenu;
    //better put it in game manager after intergration GameManager.instance.isPaused
    void Start()
    {
        InventoryMenu.gameObject.SetActive(false);
        Debug.Log("inventory setActive false");

    }


    // Update is called once per frame
    void Update()
    {
        OpenInventoryMenu();
        ToggleStatsMenu();
    }

    void OpenInventoryMenu()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I pressed");
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
    void CloseInventoryMenu()//close all menu if we gonna have any other
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

    }
    void Pause()
    {
        InventoryMenu.gameObject.SetActive(true);
        //Debug.Log("Menu setactive true");
        Time.timeScale = 0f;
        GameManager.instance.isPaused = true;
    }

    //Chris
    void ToggleStatsMenu()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            //Toggle whether the StatsMenu is active or not.
            StatPtsMenu.SetActive((StatPtsMenu.activeSelf)?false:true);
            GameManager.instance.isPaused = (StatPtsMenu.activeSelf)?true:false;
            Time.timeScale = (StatPtsMenu.activeSelf)?0f:1f;
        }
        if(StatPtsMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            StatPtsMenu.SetActive(false);
            GameManager.instance.isPaused = (StatPtsMenu.activeSelf)?true:false;
            Time.timeScale = (StatPtsMenu.activeSelf)?0f:1f;
        }


    }





}
