using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    [SerializeField] private GameObject parentRoom;
    [SerializeField] private GameObject[] Item;
    private int RNG;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void SpawnItem()
    {
        RNG = Random.Range(0, Item.Length);
        Instantiate(Item[RNG], this.gameObject.transform.position, Item[RNG].transform.rotation);
    }
}
