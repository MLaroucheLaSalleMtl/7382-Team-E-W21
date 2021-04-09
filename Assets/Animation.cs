using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimation()
    {
            this.GetComponent<Animator>().SetBool("OnTemplate", true);        
    }

    public void StopAnimation()
    {
        this.GetComponent<Animator>().SetBool("OnTemplate", false);
    }
}
