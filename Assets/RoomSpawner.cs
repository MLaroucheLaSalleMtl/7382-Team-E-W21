using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    private GameManager manager;
    [SerializeField] public int DoorDirection;
    /// <summary>
    // 1 --> North Door
    // 2 --> East Door
    // 3 --> South Door
    // 4 --> West Door
    /// </summary>

    private RoomTemplates templates;
    private int RNG;
    private bool Spawned = false;

    public float WaitTime = 4f; //This time will set a timer before destroyer the spawned points collider to avoid bugs.

    private int CollisionDoorDirection1;
    private int CollisionDoorDirection2;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,WaitTime); //This will destroy the colliders after WaitTime is done.
        manager = FindObjectOfType<GameManager>();
        templates = GameObject.Find("GameManager").GetComponent<RoomTemplates>();
        if (templates.Rooms.Count < 13)
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
        }
    }
    
    /// <summary>
    /// This is a reminder for me to know which number corresponds to which room with 2 doors;
    /// templates.CloseRoom: [1] = NE; [2] = NS; [3] = NW; [4] = ES; [5] = EW; [6] = SW; 
    /// </summary>

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomSpawnPoint")) 
        {
            if (collision.GetComponent<RoomSpawner>().Spawned == false && Spawned == false)
            {
                CollisionDoorDirection1 = this.gameObject.GetComponent<RoomSpawner>().DoorDirection;
                CollisionDoorDirection2 = collision.GetComponent<RoomSpawner>().DoorDirection;
                if (CollisionDoorDirection1 == 1 && CollisionDoorDirection2 == 2)
                {
                    Instantiate(templates.CloseRoom[1], transform.position, templates.CloseRoom[1].transform.rotation);
                }
                if (CollisionDoorDirection1 == 1 && CollisionDoorDirection2 == 3)
                {
                    Instantiate(templates.CloseRoom[2], transform.position, templates.CloseRoom[2].transform.rotation);
                }
                if (CollisionDoorDirection1 == 1 && CollisionDoorDirection2 == 4)
                {
                    Instantiate(templates.CloseRoom[3], transform.position, templates.CloseRoom[3].transform.rotation);
                }
                if (CollisionDoorDirection1 == 2 && CollisionDoorDirection2 == 3)
                {
                    Instantiate(templates.CloseRoom[4], transform.position, templates.CloseRoom[4].transform.rotation);
                }
                if (CollisionDoorDirection1 == 2 && CollisionDoorDirection2 == 4)
                {
                    Instantiate(templates.CloseRoom[5], transform.position, templates.CloseRoom[5].transform.rotation);
                }
                if (CollisionDoorDirection1 == 3 && CollisionDoorDirection2 == 3)
                {
                    Instantiate(templates.CloseRoom[6], transform.position, templates.CloseRoom[6].transform.rotation);
                }
            }
            Spawned = true;
        }
    }
}
