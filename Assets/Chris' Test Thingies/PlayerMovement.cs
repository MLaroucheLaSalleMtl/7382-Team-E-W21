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
    private AudioSource walkaudio;
    //Ming
    [SerializeField] private GameObject player;
    [SerializeField] private float dashSpeed = 7.5f, dashTime= 0, startDashTime= 0.1f, defaultspeed=10f, dashCD = 0;//deafault for gadget throwing
    [SerializeField] private bool isDashing = false,isDashCD=false;
    [SerializeField] private AudioSource AudioDash;
    public Camera Cam;
    //public Vector2 Mouse_pos;
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
        _rb.velocity = (temp * speed);
        // if(temp != new Vector2(0f,0f))
        // {
        //     animator.SetBool("Run",true);
        // }
        animator.SetBool("Run", (temp != new Vector2(0f, 0f)) ? true : false);
        sprite.flipX = (temp.x < 0f && mouse.x < this.gameObject.transform.position.x
                        || temp.x >= 0f && mouse.x < this.gameObject.transform.position.x) ? true : false;
    }

    private void _rbDash(Vector2 lookDir)
    {
        _rb.velocity = lookDir * dashSpeed;
        
        dashCD = 2;
        
        Debug.Log("Dash!");

    }


    private void Throw(Item gadget_data)//GameObject player,)
    {

        if (Input.GetKeyUp(KeyCode.R) && GameManager.instance.isPaused == false&&GameManager.instance.CurrentGagetData!=null)
        {
            
			for (int i = 0; i < GameManager.instance.items.Count; i++)//find item in inventory
			{
                if (GameManager.instance.items[i] == gadget_data&&GameManager.instance.itemNumbers[i]>0)//check item number
				{
                    Debug.Log("Throw");
                    GameObject guntip = GameObject.Find("Tip");
                    Debug.Log($"Tip position is {guntip.transform.position}");
                    GameObject gadget = Instantiate(gadget_data._Pref, guntip.transform.position, guntip.transform.rotation);
                    Rigidbody2D rb = gadget.GetComponent<Rigidbody2D>();

                    ///Force
                    rb.AddForce(guntip.transform.up * defaultspeed, ForceMode2D.Impulse);
                    //using velocity
                    //rb.velocity = new Vector3(0, 5, 0);
                    //rb.AddRelativeForce(new Vector3(0, 5, 0));
                    gadget.GetComponent<Grenade>().SetDecreasing(true);
                    GameManager.instance.RemoveItem(GameManager.instance.CurrentGagetData);
                }
                else
                {
                    Debug.Log("No gadget found in inventory.");
                }
            }
            
        }
        //}
    }
    // Update is called once per frame
    void Update()
    {
        
        //Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
        _rbMove();
        
        if (animator.GetBool("Dead")) { this.enabled = false; }
        //Play walking audio clip when player is walking/running
        if (walking && !walkaudio.isPlaying) { walkaudio.Play(); } else if (!walking) { walkaudio.Pause(); };

        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_dir = mouse - _rb.position;
        Throw(GameManager.instance.CurrentGagetData);
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
		{
            if(isDashCD==false)
			{
                isDashing = true;
            }
            dashTime = startDashTime;
            Debug.Log("leftshif press, isDashing "+isDashing);
		}
        if(isDashing)
		{
            dashTime -= Time.deltaTime;
            _rbDash(mouse_dir);
            Debug.Log("Dash time left " + dashTime);
            AudioDash.Play();
        }

        if (dashTime <= 0)
        {
            isDashing = false;
            _rb.velocity = Vector2.zero;
            //AudioDash.Stop();
            _rbMove();
            //dashTime = 0;
            
        }
        if (dashCD <= 0)
        {
            isDashCD = false;
        }
        else if(dashCD>0)
		{
            isDashCD = true;
            dashCD -= Time.deltaTime;
        }
        
    }
}
