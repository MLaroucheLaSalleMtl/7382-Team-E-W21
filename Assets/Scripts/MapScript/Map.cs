using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int NbOfMonster;
    public GameObject[] Doors;
    public GameObject ItemSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        this.DoorsController();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMonster()
    {
        NbOfMonster++;
    }

    public void ReduceMonster()
    {
        NbOfMonster--;
        DoorsController();
    }

    private void DoorsController()
    {
        if (NbOfMonster <= 0)
        {
            
            for (int i = 0; i < Doors.Length; i++)
            {
                Doors[i].GetComponent<Animator>().SetBool("IsMonsterAlive", false);
                Doors[i].GetComponent<BoxCollider2D>().isTrigger = true;
            }
            ItemSpawnPoint.GetComponent<ItemSpawner>().SpawnItem();
        }
        else
        {
            for (int i = 0; i < Doors.Length; i++)
            {
                Doors[i].GetComponent<Animator>().SetBool("IsMonsterAlive", true);
                Doors[i].GetComponent<BoxCollider2D>().isTrigger = false; 
            }
        }

    }
}
