using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    public InputAction Shoot;

    [SerializeField] private Vector3 gun_to_player_distance;
    private Vector3 mouse; //Reference to mouse position in world space
    private Vector3 p_V3_cursor; //Vector3 between Player and Cursor
    private Vector3 _player; //References player position
    [Tooltip ("Distance between the gun and the player, keep z at 0f")]



    [SerializeField] private float xoffset,yoffset;
    private float _x;
    [Tooltip("Workaround to adjust y-value of center of rotation for gun")]
    private float _y;
    [Tooltip("Workaround to adjust x-value of center of rotation for gun")]
    private float angle;
    
    private SpriteRenderer sprite; //Reference to gun sprite

    private GameObject player; //Reference to player object

    //Projectile Properties- I plan to put these on GM
    [Header ("Properties of bullet")]
    //[SerializeField] private GameObject bullet; change it to item.cs
    //[SerializeField] private int damage;
    [SerializeField] private GameObject guntip;
    [SerializeField] private float force;


    //Ming
    public bool fire = false;
    public Item CurrentWeapon_data;
    public Camera Cam;
    public Rigidbody2D Rb_player;
    float scaling = 0;

    //Chris
    private AudioSource gunaudio;

    private void Awake() 
    {
        //gameManager= GameObject.Find("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
        //this.gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        gunaudio = GetComponent<AudioSource>(); //Chris --- cache audio clip
    }
    private void GunOrientation(Vector3 distance)
    {
        this.gameObject.transform.localPosition = new Vector3((distance.x*p_V3_cursor.x)+xoffset,
                                                              (distance.y*p_V3_cursor.y)+yoffset, 0f);
        this.gameObject.transform.rotation = Quaternion.Euler(0f,0f,angle);
        /*if(CurrentWeapon_data.itemName == "Shotgun")
		{
            //flip the image
            GameObject.Find("PlayerTest").transform.GetChild(0).GetComponent<Transform>().rotation = Quaternion.Euler(0, 180f, angle);
        }
		else
		{
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }*/
    }

    private Vector3 checkCursor()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _player = player.transform.position;
        Vector3 temp = new Vector3(mouse.x - _player.x, mouse.y - _player.y,0f).normalized;

        return temp;
    }
    
    private void Flip()
    {        
       sprite.flipY = (mouse.x < _player.x)?true:false;
    }

	private void Pistol_shoot()
	{
        if (CurrentWeapon_data.Code == "Item_WP0_Pistol" && Input.GetKeyDown(KeyCode.Mouse0) && GameManager.instance.isPaused == false)
        {
            GameObject bullet_pistol = Instantiate(CurrentWeapon_data._bulletPpref, guntip.transform.position, guntip.transform.rotation);
            bullet_pistol.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            bullet_pistol.transform.eulerAngles = new Vector3(bullet_pistol.transform.eulerAngles.x, bullet_pistol.transform.eulerAngles.y, bullet_pistol.transform.eulerAngles.z - 90);
            Rigidbody2D rb = bullet_pistol.GetComponent<Rigidbody2D>();
            rb.AddForce(guntip.transform.up * 20f, ForceMode2D.Impulse);
            //Debug.Log("pistol shoot");
            //flip the image
            GameObject.Find("PlayerTest").transform.GetChild(0).GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            fire = true;
            recoil();
            gunaudio.Play(); //Chris play audio clip
        }
        else if (CurrentWeapon_data.Code == "Item_WP1_Pistol" && Input.GetKeyDown(KeyCode.Mouse0) && GameManager.instance.isPaused == false)
        {
            GameObject bullet_pistol1 = Instantiate(CurrentWeapon_data._bulletPpref, guntip.transform.position+new Vector3(0.1f,0.1f,0), guntip.transform.rotation);
            bullet_pistol1.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            bullet_pistol1.transform.eulerAngles = new Vector3(bullet_pistol1.transform.eulerAngles.x, bullet_pistol1.transform.eulerAngles.y, bullet_pistol1.transform.eulerAngles.z - 90);
            Rigidbody2D rb1 = bullet_pistol1.GetComponent<Rigidbody2D>();
            rb1.AddForce(guntip.transform.up * 20f, ForceMode2D.Impulse);

            

            GameObject bullet_pistol2 = Instantiate(CurrentWeapon_data._bulletPpref, guntip.transform.position, guntip.transform.rotation);
            bullet_pistol2.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            bullet_pistol2.transform.eulerAngles = new Vector3(bullet_pistol2.transform.eulerAngles.x, bullet_pistol2.transform.eulerAngles.y, bullet_pistol2.transform.eulerAngles.z - 90);
            Rigidbody2D rb2 = bullet_pistol2.GetComponent<Rigidbody2D>();
            rb2.AddForce(guntip.transform.up * 20f, ForceMode2D.Impulse);


            //Debug.Log("pistol shoot");
            //flip the image
            GameObject.Find("PlayerTest").transform.GetChild(0).GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            fire = true;
            recoil();
            gunaudio.Play(); //Chris play audio clip
        }
    }
    private void AR_shoot()
    {
        
        if (CurrentWeapon_data.Code == "Item_WAR0_AR" && Input.GetKey(KeyCode.Mouse0) && GameManager.instance.isPaused == false)
        {
            //bullet.transform.localScale = new Vector3(0.556f, 0.556f, 0.556f);
            GameObject bullet_AR = Instantiate(CurrentWeapon_data._bulletPpref, guntip.transform.position, guntip.transform.rotation);
            bullet_AR.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            bullet_AR.transform.eulerAngles = new Vector3(bullet_AR.transform.eulerAngles.x, bullet_AR.transform.eulerAngles.y, bullet_AR.transform.eulerAngles.z - 90);
            Rigidbody2D rb = bullet_AR.GetComponent<Rigidbody2D>();
            rb.AddForce(guntip.transform.up * 25f, ForceMode2D.Impulse);
            //Debug.Log("AR shoot");
            //flip the image
            GameObject.Find("PlayerTest").transform.GetChild(0).GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            fire = true;
            recoil();
        }
        else if (CurrentWeapon_data.Code == "Item_WAR1_AR" && Input.GetButtonDown("Fire1") && GameManager.instance.isPaused == false)
        {
            //bullet.transform.localScale = new Vector3(0.556f, 0.556f, 0.556f);
            
            GameObject bullet_AR = Instantiate(CurrentWeapon_data._bulletPpref,
                guntip.transform.position, 
                guntip.transform.rotation);
            bullet_AR.transform.eulerAngles = new Vector3(bullet_AR.transform.eulerAngles.x, 
                bullet_AR.transform.eulerAngles.y,
                bullet_AR.transform.eulerAngles.z +90);
            bullet_AR.transform.parent = guntip.transform;
            //bullet_AR.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            //Rigidbody2D rb = bullet_AR.GetComponent<Rigidbody2D>();
            //rb.AddForce(guntip.transform.up * 25f, ForceMode2D.Impulse);
            //Debug.Log("AR shoot");
            //flip the image
            GameObject.Find("PlayerTest").transform.GetChild(0).GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            fire = true;
            //recoil();
            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            //Debug.Log("mouse1 key up");
            //Destroy(GameObject.Find("Bullet_AR1(Clone)"));
            Destroy(GameObject.Find("Bullet_AR1(Clone)"));
        }
    }
    private void Shotgun_shoot()
    {
        if ((CurrentWeapon_data.Code == "Item_WS0_Shotgun" && Input.GetKeyDown(KeyCode.Mouse0) && GameManager.instance.isPaused == false))
        {
            for (int i = 0; i <= 6; i++)
            {
                GameObject bullet_shotgun = Instantiate(CurrentWeapon_data._bulletPpref, guntip.transform.position, guntip.transform.rotation);
                bullet_shotgun.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                bullet_shotgun.transform.eulerAngles = new Vector3(bullet_shotgun.transform.eulerAngles.x, bullet_shotgun.transform.eulerAngles.y, bullet_shotgun.transform.eulerAngles.z - 90);
                Rigidbody2D rb_shotgun = bullet_shotgun.GetComponent<Rigidbody2D>();
                
                switch (i)
                {
                    case 0:
                        rb_shotgun.AddForce(guntip.transform.up * 30f + new Vector3(5f, 8.7f, 0f), ForceMode2D.Impulse);
                        //Debug.Log("shotgun case0");
                        break;
                    case 1:
                        rb_shotgun.AddForce(guntip.transform.up * 30f + new Vector3(7.7f, 6.4f, 0f), ForceMode2D.Impulse);
                        //Debug.Log("shotgun case1");
                        break;
                    case 2:
                        rb_shotgun.AddForce(guntip.transform.up * 30f + new Vector3(9.4f, 3.4f, 0f), ForceMode2D.Impulse);
                        //Debug.Log("shotgun case2");
                        break;
                    case 3:
                        rb_shotgun.AddForce(guntip.transform.up * 30f + new Vector3(0f, 0f, 0f), ForceMode2D.Impulse);
                        //Debug.Log("shotgun case3");
                        break;
                    case 4:
                        rb_shotgun.AddForce(guntip.transform.up * 30f + new Vector3(9.4f, -3.4f, 0f), ForceMode2D.Impulse);
                        //Debug.Log("shotgun case4");
                        break;
                    case 5:
                        rb_shotgun.AddForce(guntip.transform.up * 30f + new Vector3(7.7f, -6.4f, 0f), ForceMode2D.Impulse);
                        //Debug.Log("shotgun case5");
                        break;
                    case 6:
                        rb_shotgun.AddForce(guntip.transform.up * 30f + new Vector3(5f, -8.7f, 0f), ForceMode2D.Impulse);
                        //Debug.Log("shotgun case6");
                        break;
                }

                fire = true;
                recoil();
                /**/
                //Debug.Log("shotgun shoot");
            }
            
            

        }
        else if(CurrentWeapon_data.Code == "Item_WS1_Shotgun")//Input.GetButtonDown("Fire1") && GameManager.instance.isPaused == false)
        {
            
            if(Input.GetButtonDown("Fire1") && GameManager.instance.isPaused == false)
            {
                scaling = 0;
                
                Debug.Log("new shotgun fire key down: "+scaling);
                
            }
            else if (Input.GetButtonUp("Fire1"))
                {
                    GameObject bullet_SG = Instantiate(CurrentWeapon_data._bulletPpref, guntip.transform.position, guntip.transform.rotation);
                    bullet_SG.transform.localScale = new Vector3(scaling, scaling, scaling);
                    bullet_SG.transform.eulerAngles = new Vector3(bullet_SG.transform.eulerAngles.x, bullet_SG.transform.eulerAngles.y, bullet_SG.transform.eulerAngles.z - 90);
                    Rigidbody2D rb = bullet_SG.GetComponent<Rigidbody2D>();
                    rb.AddForce(guntip.transform.up * 30f, ForceMode2D.Impulse);
                    fire = true;
                    Debug.Log("fire key up");
                }
           //transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 2, Time.deltaTime * 10);
            
        }
    }
    private void SniperR_shoot()
    {
        if (CurrentWeapon_data.Code == "Item_WSR0_SniperR" && Input.GetKeyDown(KeyCode.Mouse0) && GameManager.instance.isPaused == false)
        {
            //bullet.transform.localScale = new Vector3(1.27f, 1.27f, 1.27f);
            GameObject bullet_SR = Instantiate(CurrentWeapon_data._bulletPpref, guntip.transform.position, guntip.transform.rotation);
            bullet_SR.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            bullet_SR.transform.eulerAngles = new Vector3(bullet_SR.transform.eulerAngles.x, bullet_SR.transform.eulerAngles.y, bullet_SR.transform.eulerAngles.z - 90);
            Rigidbody2D rb = bullet_SR.GetComponent<Rigidbody2D>();
            rb.AddForce(guntip.transform.up * 30f, ForceMode2D.Impulse);
            fire = true;
            //Debug.Log("AR shoot");
            //flip the image
            //GameObject.Find("PlayerTest").transform.GetChild(0).GetComponent<Transform>().rotation=Quaternion.Euler(0, 180, 0);
            recoil();
        }
        else if (CurrentWeapon_data.Code == "Item_WSR1_SniperR" && Input.GetKeyDown(KeyCode.Mouse0) && GameManager.instance.isPaused == false)
        {

            //bullet.transform.localScale = new Vector3(1.27f, 1.27f, 1.27f);
            GameObject bullet_SR = Instantiate(CurrentWeapon_data._bulletPpref, guntip.transform.position, guntip.transform.rotation);
            bullet_SR.transform.localScale = new Vector3(0.1f, 0.03f, 0.06f);
            bullet_SR.transform.eulerAngles = new Vector3(bullet_SR.transform.eulerAngles.x, bullet_SR.transform.eulerAngles.y, bullet_SR.transform.eulerAngles.z - 90);
            Rigidbody2D rb = bullet_SR.GetComponent<Rigidbody2D>();
            rb.AddForce(guntip.transform.up * 30f, ForceMode2D.Impulse);
            fire = true;
            recoil();
        }
    }
    
    void UseActivatable()
	{
        if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.Activatables != null)
		{
            GameManager.instance.useItem(GameManager.instance.Activatables);
		}
        else if(Input.GetKeyDown(KeyCode.E)&&GameManager.instance.Activatables==null)
		{
            Debug.Log("Missing activatable");
		}
	}
    void recoil()
	{
        Vector2 Movement, Mouse_pos;
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 look_dir = Mouse_pos - Rb_player.position;
        if (Input.GetKey(KeyCode.Mouse0)&&fire==true)
        {
            if (CurrentWeapon_data.itemType==Item.ItemType.Pistol)
            {
                Movement.x = 0.05f;
                Movement.y = 0.05f;
                //Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
                Rb_player.MovePosition(Rb_player.position - Movement * GameManager.instance.PlayerSpeed * look_dir * 0.01f);
            }
            else if (CurrentWeapon_data.itemType == Item.ItemType.AR)
            {
                Movement.x = 0.2f;
                Movement.y = 0.2f;
                //Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
                Rb_player.MovePosition(Rb_player.position - Movement * GameManager.instance.PlayerSpeed * look_dir * 0.01f);
            }
            else if (CurrentWeapon_data.itemType == Item.ItemType.Shotgun)
            {
                Movement.x = 0.8f;
                Movement.y = 0.8f;
                //Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
                Rb_player.MovePosition(Rb_player.position - Movement * GameManager.instance.PlayerSpeed * look_dir * 0.01f);
            }
            else if (CurrentWeapon_data.itemType == Item.ItemType.SniperR)
            {
                Movement.x = 2f;
                Movement.y = 2f;
                //Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
                Rb_player.MovePosition(Rb_player.position - Movement * GameManager.instance.PlayerSpeed * look_dir * 0.01f);
            }


        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            fire = false;
        }
    }




    private void Update() 
    {
        p_V3_cursor = checkCursor();
        _y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - player.transform.position.y;
        _x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - player.transform.position.x;
        angle = Mathf.Atan2(_y,_x)*Mathf.Rad2Deg;
        GunOrientation(gun_to_player_distance);
        Flip();
        Pistol_shoot();
        AR_shoot();
        Shotgun_shoot();
        SniperR_shoot();
        //recoil();
        if(Input.GetKeyUp(KeyCode.Mouse0))
		{
            fire = false;
		}
        if(Input.GetButton("Fire1")&&CurrentWeapon_data.Code== "Item_WS1_Shotgun")
		{
            scaling += Time.deltaTime*0.1f;
            Debug.Log("scale: " + scaling);
        }
        UseActivatable();
    }
}
