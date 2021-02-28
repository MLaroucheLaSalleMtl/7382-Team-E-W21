using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStauts// : MonoBehaviour
{
    [SerializeField] private int hp_max;
    [SerializeField] private int hp;
    [SerializeField] private int lvl;
    [SerializeField] private int xp;
    [SerializeField] private int sp;
    [SerializeField] private int money;
    [SerializeField] private double baseSpeed=1;

    private Weapon my_weapon;
    private List<Item> my_items;

    public PlayerStauts(int hp_max,int hp,int lvl, int xp,int sp, int money, double baseSpeed, Weapon my_weapon,List<Item> my_items)
    {
        this.hp_max = hp_max;
        this.hp = hp;
        this.lvl = lvl;
        this.xp = xp;
        this.sp = sp;
        this.money = money;
        this.baseSpeed = baseSpeed;
        this.my_weapon = my_weapon;
        this.my_items = my_items;
    }

    void TakeDmg()
    {

    }
    void Add_Xp()
    {

    }
    void Add_Sp()
    {
        
    }
    void Levelup()
    {

    }
    void WealthChanged()
    {

    }
    void ChangeBaseSpeed()
    {

    }
    void ChangeWeapon()
    {

    }
    
}
