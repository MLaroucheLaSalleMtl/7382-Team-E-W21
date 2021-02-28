using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    private void Awake()
    {
        Instance=this;
    }


    public Transform Location;

    public Sprite PistolSprite, ARSprite, ShotgunSprite, SniperRSprite;
    public Sprite PillSprite, SyringeSprite, GrenadeSprite;
    public Sprite CoinSprite;

    public Sprite Grenade;
    public Item C0, C1;
    public Item G0;
    public Item WP0, WS0, WAR0, WSR0;
}
