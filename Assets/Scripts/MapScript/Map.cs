using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int NbOfMonster;
    public GameObject[] Doors;
    public GameObject ItemSpawnPoint;
    public GameObject[] Decoration;

    [SerializeField] public Item[] ItemList;
    [SerializeField] public Item[] WeaponList;

    private int RNG;
    public float gun_dropchance;

    //Chris BGM script reference
    private BGM_Script _BGM;
    [SerializeField] private Vector2 offset;
    private Transform p; //Player reference
    


    private bool IsPLayerOnTemplate; //this variable will determine if player is currently on the level (Template)

    // Start is called before the first frame update
    void Start()
    {
        this.DoorsController();         
        p = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //Chris
    void changeMusic()
    {
        IsPLayerOnTemplate = checkPlayerPos();
        if(IsPLayerOnTemplate)
        {
            GameManager.instance.enemiesMusic = NbOfMonster > 0;
        }
    }
    bool checkPlayerPos() //Checks if player position is inside the confined area of the map based on its position's x and y values
    {
        Vector3 p_pos = p.position;
        Vector3 m_pos = transform.position;
        bool test1 = m_pos.x-offset.x < p_pos.x && p_pos.x < m_pos.x+offset.x; //Equivalent to the width of the map
        bool test2 = m_pos.y-offset.y < p_pos.y && p_pos.y < m_pos.y + offset.y; //Equivalent to the height of the map
        
        if(test1 && test2) //if player is within the width position (interval) and height position (interval) of the map
        {
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        changeMusic();
    }

    public void AddMonster()
    {
        NbOfMonster++;
    }

    public void ReduceMonster()
    {
        NbOfMonster--;
        //BGMMusic();
        DoorsController();        
    }

    private void DoorsController()
    {
        bool nb = NbOfMonster <= 0;

        for(int i = 0; i < Doors.Length; i++)
        {
            
            if(Doors[i].GetComponent<Animator>() != null)
            {
             Doors[i].GetComponent<Animator>().SetBool("IsMonsterAlive", !nb);
            }
            Doors[i].GetComponent<BoxCollider2D>().isTrigger = nb;
        }

        if(nb)
        {
            SpawnItem();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPLayerOnTemplate = true;
            EnableDecoAnimation();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPLayerOnTemplate = false;
            DisableDecoAnimation();
        }
    }

    private void EnableDecoAnimation()
    {
        for (int i = 0; i < Decoration.Length; i++)
        {
            Decoration[i].GetComponent<Animation>().StartAnimation();
        }
    }

    private void DisableDecoAnimation()
    {
        for (int i = 0; i < Decoration.Length; i++)
        {
            Decoration[i].GetComponent<Animation>().StopAnimation();
        }
    }

    private void SpawnItem()
    {
        RNG = Random.Range(0, 1);
        if (RNG <= gun_dropchance)
        {
            RNG = Random.Range(0, WeaponList.Length);
            Instantiate(WeaponList[RNG]._Pref, ItemSpawnPoint.transform.position, ItemSpawnPoint.transform.rotation);
        }
        else
        {
            RNG = Random.Range(0, ItemList.Length);
            Instantiate(ItemList[RNG]._Pref, ItemSpawnPoint.transform.position, ItemSpawnPoint.transform.rotation);
        }
        Debug.Log("Item Spawn");
    }
}
