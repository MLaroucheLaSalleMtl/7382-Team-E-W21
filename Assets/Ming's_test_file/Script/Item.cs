using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item",fileName ="New Item")]
public class Item:ScriptableObject
{
    public string itemName;
    public enum ItemType
    {
        Weapon,
        Pistol,
        AR,
        Shotgun,
        SniperR,

        Activatable,
        Gaget,
        Syringe,

        Pill,

        Equiment,

        Coin,
        Exp_point,
        
    }
    public string itemDes;
    public int Dmg;
    public int Price;
    public float effect_value;

    public Sprite itemSprite;

    //public int amount;
    public ItemType itemType;
    public GameObject _Pref;
    public GameObject _bulletPpref;
    public string Code;

    //public Item(string name, ItemType itemType)
    //{

    //}




    public Sprite GetSprite()//Missing weapon sprite due to type sorting changed
    {
        
        switch(itemType)
        {
            default:
            //case ItemType.Pistol: return ItemAssets.Instance.PistolSprite;
            //case ItemType.AR: return ItemAssets.Instance.ARSprite;
            //case ItemType.Shotgun: return ItemAssets.Instance.ShotgunSprite;
            //case ItemType.SniperR: return ItemAssets.Instance.SniperRSprite;

            case ItemType.Coin: return ItemAssets.Instance.CoinSprite;

            case ItemType.Pill: return ItemAssets.Instance.PillSprite;
            case ItemType.Syringe: return ItemAssets.Instance.SyringeSprite;
        }
    }
    
}
