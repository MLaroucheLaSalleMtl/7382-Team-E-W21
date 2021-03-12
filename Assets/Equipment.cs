using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] public Item CurrentWeaponData, CurrentGagetData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentWeaponData = this.gameObject.transform.GetChild(0).GetComponent<GunScript>().CurrentWeapon_data;
     }
}
