using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    private GameManager _gm;
    void Start() //Assistance from Marc-André Larouche to fix GameManager bug in Main Menu
    {
        _gm = GameManager.instance;

        if(_gm != null)
        {
            Destroy(_gm);
            Destroy(_gm.gameObject);
        }
    }
    
}
