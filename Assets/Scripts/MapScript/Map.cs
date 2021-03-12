using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int NbOfMonster;
    public GameObject[] Doors;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DoorsController", 0.5f);
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
                Doors[i].GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < Doors.Length; i++)
            {
                Doors[i].GetComponent<BoxCollider2D>().enabled = false; 
            }
        }
    }
}
