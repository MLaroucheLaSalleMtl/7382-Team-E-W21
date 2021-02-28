using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item",fileName ="New Item")]
public class Item:ScriptableObject
{
    public string itemName,itemDes;
    public Sprite itemSprite;
    public enum ItemType
    {
        Pistol,
        AR,
        Shotgun,
        SniperR,

        Coin,
        Exp_point,
        Pill,
        Syringe,


    }

    //public int amount;
    public ItemType itemType;


    //public Item(string name, ItemType itemType)
    //{

    //}




    public Sprite GetSprite()
    {
        
        switch(itemType)
        {
            default:
            case ItemType.Pistol: return ItemAssets.Instance.PistolSprite;
            case ItemType.AR: return ItemAssets.Instance.ARSprite;
            case ItemType.Shotgun: return ItemAssets.Instance.ShotgunSprite;
            case ItemType.SniperR: return ItemAssets.Instance.SniperRSprite;

            case ItemType.Coin: return ItemAssets.Instance.CoinSprite;

            case ItemType.Pill: return ItemAssets.Instance.PillSprite;
            case ItemType.Syringe: return ItemAssets.Instance.SyringeSprite;
        }
    }
    
}
