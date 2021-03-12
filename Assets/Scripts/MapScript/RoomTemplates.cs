using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public List<GameObject> Rooms;
    [SerializeField] public GameObject[] SouthRooms;
    [SerializeField] public GameObject[] NorthRooms;
    [SerializeField] public GameObject[] EastRooms;
    [SerializeField] public GameObject[] WestRooms;

    [SerializeField] public GameObject[] CloseRoom;

    private bool SpawnedBoss = false;
    [SerializeField] public float WaitTime;
    [SerializeField] public GameObject[] Boss;
    private int DoorDirect;

    void Update()
    {
        if (WaitTime <= 0)
        {
            for (int i = 0; i < Rooms.Count; i++)
            {
                if (i == Rooms.Count - 1 && SpawnedBoss == false)
                {
                    //Boss[1] --> N; Boss[2] --> E; Boss[3] --> S; Boss[4] --> W;
                    //If the last room had a door pointing up, then for the boss room, I need to room to have a door poiting down;
                    DoorDirect = Rooms[i].GetComponentInChildren<RoomSpawner>().DoorDirection;
                    if (DoorDirect == 1)
                    {

                        Instantiate(Boss[3], Rooms[i].transform.transform.position, Quaternion.identity);
                    }
                    if (DoorDirect == 2)
                    {
                        Instantiate(Boss[2], Rooms[i].transform.transform.position, Quaternion.identity);
                    }
                    if (DoorDirect == 3)
                    {
                        Instantiate(Boss[0], Rooms[i].transform.transform.position, Quaternion.identity);
                    }
                    if (DoorDirect == 4)
                    {
                        Instantiate(Boss[1], Rooms[i].transform.transform.position, Quaternion.identity);
                    }
                    SpawnedBoss = true;
                }
            }
        }
        else
        {
            WaitTime -= Time.deltaTime;
        }
    }

}
