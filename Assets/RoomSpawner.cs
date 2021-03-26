using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int DoorDirection;
    // 1 = North
    // 2 = East
    // 3 = South
    // 4 = Ouest
    private int RNG;
    private RoomTemplates templates;
    private bool spawned = false;
    private int CollisionDirection;

    //-------------------------------------------------
    private int top;
    private int right;
    private int Bottom;
    private int Left;
    [SerializeField] private GameObject LastTopTemplate;
    [SerializeField] private GameObject LastRightTemplate;
    [SerializeField] private GameObject LastBottomTemplate;
    [SerializeField] private GameObject LastLeftTemplate;

    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.Find("RoomManager").GetComponent<RoomTemplates>();
        Invoke("SpawnRoom", 0.1f);
        top = 0;
        right = 0;
        Bottom = 0;
        Left = 0;
    }

    // Update is called once per frame
    private void SpawnRoom()
    {
        if (spawned == false)
        {
            if (DoorDirection == 1)
            {
                //Spawn room with a bottom door
                RNG = Random.Range(0, templates.RoomSouth.Length);
                LastBottomTemplate = templates.RoomSouth[RNG];
                Instantiate(templates.RoomSouth[RNG], transform.position, templates.RoomSouth[RNG].transform.rotation);
            }
            else if (DoorDirection == 2)
            {
                //Spawn room with a left door
                RNG = Random.Range(0, templates.RoomOuest.Length);
                LastLeftTemplate = templates.RoomOuest[RNG];
                Instantiate(templates.RoomOuest[RNG], transform.position, templates.RoomOuest[RNG].transform.rotation);
            }
            else if (DoorDirection == 3)
            {
                //Spawn room with a top door
                RNG = Random.Range(0, templates.RoomNorth.Length);
                LastTopTemplate = templates.RoomNorth[RNG];
                Instantiate(templates.RoomNorth[RNG], transform.position, templates.RoomNorth[RNG].transform.rotation);
            }
            else if (DoorDirection == 4)
            {
                //Spawn room with a right door
                RNG = Random.Range(0, templates.RoomEast.Length);
                LastRightTemplate = templates.RoomEast[RNG];
                Instantiate(templates.RoomEast[RNG], transform.position, templates.RoomEast[RNG].transform.rotation);
            }
            spawned = true;
        }
        
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomSpawnPoint"))
        {
            if (collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                CollisionDirection = collision.GetComponent<RoomSpawner>().DoorDirection;
                if (this.DoorDirection == 1 && CollisionDirection == 2 || CollisionDirection == 1 && this.DoorDirection == 2)
                {
                    Instantiate(templates.RoomSouth[3], transform.position, templates.RoomSouth[3].transform.rotation);
                }
                else if (this.DoorDirection == 1 && CollisionDirection == 3 || this.DoorDirection == 3 && CollisionDirection == 1)
                {
                    Instantiate(templates.RoomSouth[1], transform.position, templates.RoomSouth[1].transform.rotation);
                }
                else if (this.DoorDirection == 1 && CollisionDirection == 4 || this.DoorDirection == 4 && CollisionDirection == 1)
                {
                    Instantiate(templates.RoomSouth[2], transform.position, templates.RoomSouth[2].transform.rotation);
                }
                else if (this.DoorDirection == 2 && CollisionDirection == 3 || this.DoorDirection == 3 && CollisionDirection == 2)
                {
                    Instantiate(templates.RoomOuest[2], transform.position, templates.RoomOuest[2].transform.rotation);
                }
                else if (this.DoorDirection == 2 && CollisionDirection == 4 || this.DoorDirection == 4 && CollisionDirection == 2)
                {
                    Instantiate(templates.RoomOuest[3], transform.position, templates.RoomOuest[3].transform.rotation);
                }
                else if (this.DoorDirection == 3 && CollisionDirection == 4 || this.DoorDirection == 4 && CollisionDirection == 3)
                {
                    Instantiate(templates.RoomNorth[2], transform.position, templates.RoomNorth[2].transform.rotation);
                }
                this.spawned = true;
                collision.GetComponent<RoomSpawner>().spawned = true;
            }
            else if (collision.GetComponent<RoomSpawner>().spawned == true && spawned == false)
            {
                if (this.DoorDirection == 1)
                {
                    //Spawn room with a bottom door
                    RNG = Random.Range(0, templates.RoomSouth.Length);
                    Instantiate(templates.RoomSouth[RNG], LastBottomTemplate.GetComponent<Transform>().position, templates.RoomSouth[RNG].transform.rotation);
                    
                    LastBottomTemplate = templates.RoomSouth[RNG];
                }
                else if (this.DoorDirection == 2)
                {
                    //Spawn room with a left door
                    RNG = Random.Range(0, templates.RoomOuest.Length);
                    Instantiate(templates.RoomOuest[RNG], LastBottomTemplate.GetComponent<Transform>().position, templates.RoomOuest[RNG].transform.rotation);
                    LastBottomTemplate = templates.RoomSouth[RNG];
                }

            }
            
        }
    }
}
