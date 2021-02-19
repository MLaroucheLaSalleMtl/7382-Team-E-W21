using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSort : MonoBehaviour //Courtesy of Marc
{

    [SerializeField] private int sortingOrderBase = 5000;
    [SerializeField] private int scalingOrder = 100;
    [SerializeField] private int offset = 0;
    [SerializeField] private bool sortOnce = true;
    [SerializeField] private bool userOffset = false;
    private GameManager manager;

    private float refreshTimer = 0.1f;
    private Renderer myRenderer;

    public int Offset { get => offset; set => offset = value; }
    public int SortingOrderBase { get => sortingOrderBase; set => sortingOrderBase = value; }
    public int ScalingOrder { get => scalingOrder; set => scalingOrder = value; }

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>(); //Cache the component
        InvokeRepeating("Refresh",0, refreshTimer); //Call Refresh() function repeatedly every refreshTimer time.   
        manager = GameManager.instance; //Cache the GameManager
        if(userOffset) offset = manager.Offset;
        sortingOrderBase = manager.SortingOrderBase;
        scalingOrder = manager.ScalingOrder;
    }

    // Update is called once per frame
    void Refresh()
    {
        //Change order in layer according to Y position value of object
        myRenderer.sortingOrder = (int)(sortingOrderBase - (transform.position.y * scalingOrder) - offset);         
        if(sortOnce)
        {
            Destroy(this); //Delete script instead of gameObject            
        }
    }
}
