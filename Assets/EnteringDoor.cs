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
    private bool Open;

    void OpenDoors(int MonsterAlive)
    {
        if (MonsterAlive <= 0)
        {
            Open = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Open == true) //this allows to confirm that it only works with the player and not with the monsters, and that there are no monsters in the level left
        {
            switch (Doordirection)
            {
                case 1:
                    collision.transform.position += new Vector3(0, 8, 0);
                    break;
                case 2:
                    collision.transform.position += new Vector3(8, 0, 0);
                    break;
                case 3:
                    collision.transform.position += new Vector3(0, -8, 0);
                    break;
                case 4:
                    collision.transform.position += new Vector3(-8, 0, 0);
                    break;
            }
                
            collision.transform.position += new Vector3(8, 0, 0);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Open = false; 
    }
}
