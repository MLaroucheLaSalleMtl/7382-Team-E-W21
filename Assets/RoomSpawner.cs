using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    private GameManager manager;
    [SerializeField] public int DoorDirection;
    /// <summary>
    // 1 --> Top Door
    // 2 --> Right Door
    // 3 --> Bottom Door
    // 4 --> Left Door
    /// </summary>

    private RoomTemplates templates;
    private int RNG;
    private bool Spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        templates = GameObject.Find("GameManager").GetComponent<RoomTemplates>();
        if (manager.NbMapTemplates<10)
        {
            Invoke("SpawnRoom", 0.1f);
        }
    }

    // Update is called once per frame
    void SpawnRoom()
    {
        if (Spawned == false)
        {
            switch (DoorDirection)
            {
                case 1:
                    //Spawn room with a bottom door
                    RNG = Random.Range(0, templates.SouthRooms.Length);
                    Instantiate(templates.SouthRooms[RNG], transform.position, templates.SouthRooms[RNG].transform.rotation);
                    break;
                case 2:
                    //Spawn room with a left door
                    RNG = Random.Range(0, templates.WestRooms.Length);
                    Instantiate(templates.WestRooms[RNG], transform.position, templates.WestRooms[RNG].transform.rotation);
                    break;
                case 3:
                    //Spawn room with top door
                    RNG = Random.Range(0, templates.NorthRooms.Length);
                    Instantiate(templates.NorthRooms[RNG], transform.position, templates.NorthRooms[RNG].transform.rotation);
                    break;
                case 4:
                    //Spawn room with right door
                    RNG = Random.Range(0, templates.EastRooms.Length);
                    Instantiate(templates.EastRooms[RNG], transform.position, templates.EastRooms[RNG].transform.rotation);
                    break;
                    
            }
            Spawned = true;
            manager.NbMapTemplates++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomSpawnedPoint") && collision.GetComponent<RoomSpawner>().Spawned == true)
        {
            Destroy(gameObject);
        }
    }
}
