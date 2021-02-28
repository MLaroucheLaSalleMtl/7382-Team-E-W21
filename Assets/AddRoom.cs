using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;
    void Start()
    {
        templates = GameObject.Find("GameManager").GetComponent<RoomTemplates>();
        templates.Rooms.Add(this.gameObject);
    }
}
