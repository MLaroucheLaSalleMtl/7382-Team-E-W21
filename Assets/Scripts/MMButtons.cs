using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MMButtons : MonoBehaviour
{


    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void SettingsBtn()
    {
        //SceneManager.LoadScene();
    }
    public void QuitBtn()
    {
        Application.Quit();
    }
}
