using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteringDoor : MonoBehaviour
{
    /// <summary>
    /// 1 --> North
    /// 2 --> East
    /// 3 --> south
    /// 4 --> West
    /// </summary>
    [SerializeField] private int Doordirection;
    [SerializeField] private GameObject Zone;

    void OpenDoors(int MonsterAlive)
    {
        if (MonsterAlive > 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //this allows to confirm that it only works with the player and not with the monsters, and that there are no monsters in the level left
        {
            switch (Doordirection)
            {
                case 1:
                    collision.transform.position += new Vector3(0, 8, 0);
                    Zone.transform.position += new Vector3(0, 28, 0);
                    break;
                case 2:
                    collision.transform.position += new Vector3(8, 0, 0);
                    Zone.transform.position += new Vector3(48, 0, 0);
                    break;
                case 3:
                    collision.transform.position += new Vector3(0, -8, 0);
                    Zone.transform.position += new Vector3(-28, 0, 0);
                    break;
                case 4:
                    collision.transform.position += new Vector3(-8, 0, 0);
                    Zone.transform.position += new Vector3(0, -48, 0);
                    break;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
      //  OpenDoors();
    }
}
