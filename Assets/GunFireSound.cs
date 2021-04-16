using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFireSound : MonoBehaviour
{

    //[SerializeField] AudioClip WP0, WP1, WAR0, WAR1, WS0, WS1, WSR0, WSR1;
    [SerializeField] AudioSource aduio0, audio1;
    [SerializeField] bool audioloop;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audio0 = GetComponent<AudioSource>();
        AudioSource audio1 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
