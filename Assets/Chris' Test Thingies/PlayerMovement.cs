using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Inputs
    [Header ("Input Actions")]
    public InputAction move;
    

    //Unchangeable variables
    private Rigidbody2D _rb; 
    private Animator animator;
    private SpriteRenderer sprite;
    private Vector2 mouse;
    private float speed;
    //Ming
    [SerializeField] private GameObject player;
    public Camera Cam;
    public Vector2 Mouse_pos;


    // Start is called before the first frame update
    void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        speed = GameManager.instance.PlayerSpeed;
    }

     private void OnEnable() 
    {
        move.Enable();
    }

    private void _rbMove()
    {
        Vector2 temp = move.ReadValue<Vector2>();
        _rb.velocity = temp*speed;
        // if(temp != new Vector2(0f,0f))
        // {
        //     animator.SetBool("Run",true);
        // }
        animator.SetBool("Run", (temp != new Vector2(0f,0f))?true:false);
        sprite.flipX = (temp.x < 0f  && mouse.x < this.gameObject.transform.position.x 
                        || temp.x >= 0f && mouse.x < this.gameObject.transform.position.x)?true:false;
    }
    
    private void _rbDash(GameObject player,Vector2 lookDir)
    {
        //Vector2 temp = dash.ReadValue<Vector2>();
        float x = lookDir.x;
        float y = lookDir.y;
        while (lookDir.x * lookDir.x + lookDir.y * lookDir.y > 2f)
        {
            x = lookDir.x * 0.8f;
            y = lookDir.y * 0.8f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift) & GameManager.instance.isPaused == false)
        {
            player.GetComponent<Transform>().position = new Vector2(player.transform.position.x + x, player.transform.position.y + y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
        _rbMove();
        //_rbDash(player, Mouse_pos); //for the demo, i suggest we postpone this function, I couldn't find the solution
        if(animator.GetBool("Dead")){this.enabled = false;}
    }
}
