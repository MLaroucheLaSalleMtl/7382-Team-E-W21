using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_activatables : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayActivatables();
    }
    void DisplayActivatables()
	{
        //Debug.Log("DisplayAct");
        if(GameManager.instance.Gadget ==null)
		{
            this.gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(1,1,1,0);
            
        }
		else
		{
            this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.Gadget.itemSprite;
            this.gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        if (GameManager.instance.Activatables== null)
        {
            this.gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
		else
		{
            this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = GameManager.instance.Activatables.itemSprite;
            this.gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }
}
