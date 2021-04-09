using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Christopher's Script
public class LevelUpText_Script : MonoBehaviour
{

    //private Text _LvlUp;
    [Range (0f,1f)] [SerializeField] private float alpha = 1f;
    [Range (0f,1f)] [SerializeField] private float changing_value;
    [Range (0f,0.5f)] [SerializeField] private float minimum_a;
    [Range (0.5f,1f)] [SerializeField] private float maximum_a;
    [SerializeField] private float pulsing_time;
    private Text textAlpha;    
    private bool increase;
    [SerializeField] private Text bonusText;

    // Start is called before the first frame update
    void Start()
    {
        //_LvlUp = gameObject.GetComponent<Text>();
        textAlpha = gameObject.GetComponent<Text>();
        StartCoroutine("ChangeAlpha");  
        
        //StartCoroutine("AlphaDown");
        
    }    
    
    IEnumerator ChangeAlpha()
    {
        //Debug.Log("ChangeAlpha Coroutine has Started");
        
        if(alpha >= maximum_a || alpha <= minimum_a){increase = (alpha <= minimum_a);}  //Change bool value to determine increase OR decrease in alpha only when reaching max/min
        alpha += increase?changing_value:-changing_value;
        Vector4 change = new Vector4(255f,255f,255f,alpha);
        textAlpha.color = change;
        bonusText.color = change;
        
        yield return new WaitForSeconds(pulsing_time); //Amount of time to wait before increase/decrease in alpha value
        
        StartCoroutine("ChangeAlpha");
    }


    
    // Update is called once per frame
    void Update()
    {   
        //textAlpha.color = new Vector4(255f,255f,255f,alpha);
        //textAlpha.color = new Vector4(255f,255f,255f,alpha);
    }
}
