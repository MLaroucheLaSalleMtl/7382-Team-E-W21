using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Items;

    private int RNG;

    public void SpawnItem(int ChosenItem)
    {
         RNG = Random.Range(0, Items.Length);
        Instantiate(Items[RNG], transform.position, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
