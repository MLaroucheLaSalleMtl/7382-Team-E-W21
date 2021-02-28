using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject InventoryMenu;
    //better put it in game manager after intergration GameManager.instance.isPaused
    void Start()
    {
        InventoryMenu.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        OpenInventoryMenu();
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
        Debug.Log("Menu setactive false");
        Time.timeScale = 1f;
        GameManager.instance.isPaused = false;

    }
    void Pause()
    {
        InventoryMenu.gameObject.SetActive(true);
        Debug.Log("Menu setactive true");
        Time.timeScale = 0f;
        GameManager.instance.isPaused = true;
    }






}
