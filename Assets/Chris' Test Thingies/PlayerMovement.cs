using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Inputs
    [Header("Input Actions")]
    public InputAction move;


    //Unchangeable variables
    private Rigidbody2D _rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private Vector2 mouse, mouse_dir;
    private float speed;
    //Ming
    [SerializeField] private GameObject player;
    public Camera Cam;
    //public Vector2 Mouse_pos;
    private AudioSource walkaudio;
    private bool walking;


    //Start is called before the first frame update
    void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        speed = GameManager.instance.PlayerSpeed;
        walkaudio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        move.Enable();
    }

    public void _updateSpeed()
    {
        speed = GameManager.instance.PlayerSpeed;
    }
    private void _rbMove()
    {
        Vector2 temp = move.ReadValue<Vector2>();
        walking = (temp == Vector2.zero) ? false : true;
        _rb.velocity = temp * speed;
        // if(temp != new Vector2(0f,0f))
        // {
        //     animator.SetBool("Run",true);
        // }
        animator.SetBool("Run", (temp != new Vector2(0f, 0f)) ? true : false);
        sprite.flipX = (temp.x < 0f && mouse.x < this.gameObject.transform.position.x
                        || temp.x >= 0f && mouse.x < this.gameObject.transform.position.x) ? true : false;
    }

    private void _rbDash(GameObject player, Vector2 lookDir)
    {
        //Vector2 temp = dash.ReadValue<Vector2>();

        float x = lookDir.x;
        float y = lookDir.y;
        while (x * x + y * y > 4f)
        {
            x = x * 0.8f;
            y = y * 0.8f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift) & GameManager.instance.isPaused == false)
        {
            player.GetComponent<Transform>().position = new Vector2(player.transform.position.x + x, player.transform.position.y + y);
        }
    }


    private void Throw(Item gadget_data, Vector2 lookdir)//GameObject player,)
    {
        //if (Input.GetKeyUp(KeyCode.R))// & player.gadget != null & player.gadgetCD = true)
        //{
        //player.UseGadget();//<----,player.gadgetCD = x;

        if (Input.GetKeyUp(KeyCode.R) && GameManager.instance.isPaused == false&&GameManager.instance.Gadget!=null)
        {
            if(GameManager.instance.Gadget!=null)
			{
                Debug.Log("Throw");
                float defaultspeed = 10f;
                //firePoint = Guntip.transform;
                GameObject guntip = GameObject.Find("Tip");
                GameObject gadget = Instantiate(gadget_data._Pref, guntip.transform.position, guntip.transform.rotation);
                Rigidbody2D rb = gadget.GetComponent<Rigidbody2D>();
                //Force
                rb.AddForce(guntip.transform.up * defaultspeed, ForceMode2D.Impulse);
                //velocity
                //rb.velocity = new Vector3(0, 5, 0);
                //rb.AddRelativeForce(new Vector3(0, 5, 0));
                gadget.GetComponent<Grenade>().SetDecreasing();
                GameManager.instance.RemoveItem(GameManager.instance.Gadget);
            }
            else
			{
                Debug.Log("No gadget found in inventory.");
			}
        }
        //}
    }
    // Update is called once per frame
    void Update()
    {
        mouse_dir = mouse - _rb.position;
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
        _rbMove();
        _rbDash(player, mouse_dir); //for the demo, i suggest we postpone this function, I couldn't find the solution
        if (animator.GetBool("Dead")) { this.enabled = false; }
        //Play walking audio clip when player is walking/running
        if (walking && !walkaudio.isPlaying) { walkaudio.Play(); } else if (!walking) { walkaudio.Pause(); };
        Throw(GameManager.instance.Gadget, mouse_dir);

    }
}
