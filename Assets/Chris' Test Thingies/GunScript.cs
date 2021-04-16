using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    //Chris Variables (START) --------
    public InputAction Shoot;

    [SerializeField] private Vector3 gun_to_player_distance;
    private Vector3 mouse; //Reference to mouse position in world space
    private Vector3 p_V3_cursor; //Vector3 between Player and Cursor
    private Vector3 _player; //References player position
    [Tooltip("Distance between the gun and the player, keep z at 0f")]



    [SerializeField] private float xoffset, yoffset;
    private float _x;
    [Tooltip("Workaround to adjust y-value of center of rotation for gun")]
    private float _y;
    [Tooltip("Workaround to adjust x-value of center of rotation for gun")]
    private float angle;


    private SpriteRenderer sprite; //Reference to gun sprite

    private GameObject player; //Reference to player object
    //Chris Variables (END)--------------

    //Projectile Properties- I plan to put these on GM
    [Header("Properties of bullet")]
    //[SerializeField] private GameObject bullet; change it to item.cs
    //[SerializeField] private int damage;
    [SerializeField] private GameObject guntip;
    [SerializeField] private float force;


    //Ming
    //public bool fire = false; move to GM
    public Item CurrentWeapon_data;
    public Camera Cam;
    public Rigidbody2D Rb_player;
    [SerializeField] AudioSource audio0, audio1;
    //float scaling = 0; move to GM.cs

    //Chris
    //private AudioSource gunaudio;

    //Bryan
    private bool Once = true;
    public int Ammunition;
    private bool ReloadOnce = true;
    [SerializeField] AudioSource ReloadSoundEffect;
    public Text TextAmmo;

    IEnumerator FireRate(float FireRateTime) //Bryan //Block the ability to spam the fire button to shoot
    {
        Once = false;
        yield return new WaitForSeconds(FireRateTime);
        Once = true;
    }

    IEnumerator ReloadGun(float ReloadTime)//Bryan // Reload Gun when current Ammo is 0
    {
        ReloadOnce = false;
        ReloadSoundEffect.clip = CurrentWeapon_data.GunReloadAudio;
        ReloadSoundEffect.Play();
        //Debug.Log("Reload Audio Played");
        yield return new WaitForSeconds(ReloadTime);
        Ammunition = CurrentWeapon_data.MaxAmmo;
        TextAmmo.text = Ammunition + "/" + CurrentWeapon_data.MaxAmmo;
        Once = true;
    }

    private void Awake()
    {
        //gameManager= GameObject.Find("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
        //this.gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        //gunaudio = GetComponent<AudioSource>(); //Chris --- cache audio clip
        audio0 = GetComponent<AudioSource>();
        audio1 = GetComponent<AudioSource>();

        //Bryan
        Ammunition = 7;
    }
    private void GunOrientation(Vector3 distance)
    {
        this.gameObject.transform.localPosition = new Vector3((distance.x * p_V3_cursor.x) + xoffset,
                                                              (distance.y * p_V3_cursor.y) + yoffset, 0f);
        this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
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

    private Vector3 checkCursor() //Chris --------
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _player = player.transform.position;
        Vector3 temp = new Vector3(mouse.x - _player.x, mouse.y - _player.y, 0f).normalized;

        return temp;
    }

    private void Flip() //Chris ----------
    {
        sprite.flipY = (mouse.x < _player.x) ? true : false;
    }

    private void Pistol_shoot()
    {
        if (GameManager.instance.isPaused == false && Once)
        {
            //default pistol
            if (CurrentWeapon_data.Code == "Item_WP0_Pistol")
            {
                GameObject bullet_pistol = Instantiate(CurrentWeapon_data._bulletPpref,
                    guntip.transform.position,
                    guntip.transform.rotation);

                bullet_pistol.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);

                bullet_pistol.transform.eulerAngles = new Vector3(bullet_pistol.transform.eulerAngles.x,
                    bullet_pistol.transform.eulerAngles.y,
                    bullet_pistol.transform.eulerAngles.z - 90);

                Rigidbody2D rb = bullet_pistol.GetComponent<Rigidbody2D>();
                rb.AddForce(guntip.transform.up * 20f, ForceMode2D.Impulse);
                //Debug.Log("pistol shoot");
                //flip the image
                GameObject.Find("PlayerTest").transform.GetChild(0).GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                GameManager.instance.fire = true;
                recoil(0.2f);
                //gunaudio.Play(); //Chris play audio clip
                Gun_sound_play(GameManager.instance.CurrentWeaponData.Code);
                //Bryan
                Ammunition -= 1;

            }
            //BlueRose
            else if (CurrentWeapon_data.Code == "Item_WP1_Pistol")
            {
                //First bullet
                GameObject bullet_pistol1 = Instantiate(CurrentWeapon_data._bulletPpref,
                    guntip.transform.position + new Vector3(0.1f, 0.1f, 0),
                    guntip.transform.rotation);
                bullet_pistol1.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                bullet_pistol1.transform.eulerAngles = new Vector3(bullet_pistol1.transform.eulerAngles.x,
                    bullet_pistol1.transform.eulerAngles.y,
                    bullet_pistol1.transform.eulerAngles.z - 90);

                Rigidbody2D rb1 = bullet_pistol1.GetComponent<Rigidbody2D>();
                rb1.AddForce(guntip.transform.up * 20f, ForceMode2D.Impulse);

                //Second bullet
                GameObject bullet_pistol2 = Instantiate(CurrentWeapon_data._bulletPpref,
                    guntip.transform.position,
                    guntip.transform.rotation);

                bullet_pistol2.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);

                bullet_pistol2.transform.eulerAngles = new Vector3(bullet_pistol2.transform.eulerAngles.x,
                    bullet_pistol2.transform.eulerAngles.y,
                    bullet_pistol2.transform.eulerAngles.z - 90);

                Rigidbody2D rb2 = bullet_pistol2.GetComponent<Rigidbody2D>();
                rb2.AddForce(guntip.transform.up * 20f, ForceMode2D.Impulse);
                //Debug.Log("pistol shoot");
                //flip the image
                GameObject.Find("PlayerTest").transform.GetChild(0).GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                GameManager.instance.fire = true;
                recoil(0.5f);
                //gunaudio.Play(); //Chris play audio clip
                Gun_sound_play(GameManager.instance.CurrentWeaponData.Code);
                //Bryan
                Ammunition -= 1;
            }
            //Bryan
            StartCoroutine("FireRate", GameManager.instance.CurrentWeaponData.FireRateTime); 
        }
    }


    //Chris ----- Variables and Coroutine for Modifications of AR

    bool delay = false;

    IEnumerator DelayBullet(float t)
    {
        delay = true;
        yield return new WaitForSeconds(t);
        delay = false;
        Debug.Log("Delaying bullet for " + t+" second.");
    }

    //Chris ----- End of modifications of AR


    private void AR_shoot()
    {

        if (GameManager.instance.isPaused == false)
        {
            if (CurrentWeapon_data.Code == "Item_WAR0_AR" && Once)
            {
                if (delay == false) //Chris --- Used to delay 
                {
                    Vector3 gape= new Vector3(0.3f, 0, 0);
                    //Ming, it should have a better way doing, but need to trace the trace guntip position and add xyz coresponding circular movement of guntip
                    //1st
                    GameObject bullet_AR1 = Instantiate(CurrentWeapon_data._bulletPpref, guntip.transform.position + gape*3, guntip.transform.rotation);
                    bullet_AR1.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                    bullet_AR1.transform.eulerAngles = new Vector3(bullet_AR1.transform.eulerAngles.x, bullet_AR1.transform.eulerAngles.y, bullet_AR1.transform.eulerAngles.z - 90);

                    Rigidbody2D rb1 = bullet_AR1.GetComponent<Rigidbody2D>();
                    rb1.AddForce(guntip.transform.up * 25f, ForceMode2D.Impulse);
                    //2nd
                    GameObject bullet_AR2 = Instantiate(CurrentWeapon_data._bulletPpref, guntip.transform.position + gape * 2, guntip.transform.rotation);
                    bullet_AR2.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                    bullet_AR2.transform.eulerAngles = new Vector3(bullet_AR2.transform.eulerAngles.x, bullet_AR2.transform.eulerAngles.y, bullet_AR2.transform.eulerAngles.z - 90);

                    Rigidbody2D rb2 = bullet_AR2.GetComponent<Rigidbody2D>();
                    rb2.AddForce(guntip.transform.up * 25f, ForceMode2D.Impulse);
                    //3rd
                    GameObject bullet_AR3 = Instantiate(CurrentWeapon_data._bulletPpref, guntip.transform.position + gape, guntip.transform.rotation);
                    bullet_AR3.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                    bullet_AR3.transform.eulerAngles = new Vector3(bullet_AR3.transform.eulerAngles.x, bullet_AR3.transform.eulerAngles.y, bullet_AR3.transform.eulerAngles.z - 90);

                    Rigidbody2D rb3 = bullet_AR3.GetComponent<Rigidbody2D>();
                    rb3.AddForce(guntip.transform.up * 25f, ForceMode2D.Impulse);

                    //Debug.Log("AR shoot");
                    //flip the image
                    GameObject.Find("PlayerTest").transform.GetChild(0).GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                    GameManager.instance.fire = true;
                    recoil(1f);
                    Gun_sound_play(GameManager.instance.CurrentWeaponData.Code);

                    Debug.Log("AR one bullet spawn");
                    //Bryan
                    Ammunition -= 1;
                    //StartCoroutine("DelayBullet", 0.7f); //After firing maxShots, it needs to chillllllll                                        
                }

                StartCoroutine("FireRate", GameManager.instance.CurrentWeaponData.FireRateTime);

            }
            else if (CurrentWeapon_data.Code.Contains("WAR1") && Once)
            {
                //bullet.transform.localScale = new Vector3(0.556f, 0.556f, 0.556f);

                GameObject bullet_AR = Instantiate(CurrentWeapon_data._bulletPpref,
                    guntip.transform.position,
                    guntip.transform.rotation);

                //rotate the gunTip
                bullet_AR.transform.eulerAngles = new Vector3(bullet_AR.transform.eulerAngles.x,
                    bullet_AR.transform.eulerAngles.y,
                    bullet_AR.transform.eulerAngles.z + 90);
                bullet_AR.transform.parent = guntip.transform;
                //Debug.Log("AR shoot");
                //flip the image
                //GameObject.Find("PlayerTest").transform.GetChild(0).GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                GameManager.instance.fire = true;
                recoil(0.3f);
                Gun_sound_play(GameManager.instance.CurrentWeaponData.Code);
                //Bryan 
                Ammunition -= 1;
                StartCoroutine("FireRate", GameManager.instance.CurrentWeaponData.FireRateTime);
            }
        }

    }
    private void Shotgun_shoot()
    {
        if (GameManager.instance.isPaused == false)
        {
            //normal shotgun
            if (CurrentWeapon_data.Code == "Item_WS0_Shotgun" && Once)
            {
                for (int i = 0; i <= 6; i++)
                {
                    GameObject bullet_shotgun = Instantiate(CurrentWeapon_data._bulletPpref,
                        guntip.transform.position, guntip.transform.rotation);
                    bullet_shotgun.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);

                    bullet_shotgun.transform.eulerAngles = new Vector3(bullet_shotgun.transform.eulerAngles.x,
                        bullet_shotgun.transform.eulerAngles.y,
                        bullet_shotgun.transform.eulerAngles.z - 90);
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

                    GameManager.instance.fire = true;
                    recoil(1f);
                    Gun_sound_play(GameManager.instance.CurrentWeaponData.Code);
                    /**/
                    //Debug.Log("shotgun shoot");
                }
                //Bryan
                Ammunition -= 1;
                StartCoroutine("FireRate", GameManager.instance.CurrentWeaponData.FireRateTime);
            }
            //chargable shotgun
            else if (CurrentWeapon_data.Code == "Item_WS1_Shotgun")//Input.GetButtonDown("Fire1") && GameManager.instance.isPaused == false)
            {
                
                if (Input.GetButtonDown("Fire1") && GameManager.instance.isPaused == false)
                {
                    GameManager.instance.scaling = 0;
                    audio1.clip = GameManager.instance.CurrentWeaponData._ItemSound1;
                    audio1.Play();
                    //Debug.Log("new shotgun fire key down: " + GameManager.instance.scaling);

                }
                else if (Input.GetButtonUp("Fire1") && Once)
                {
                    GameObject bullet_SG = Instantiate(CurrentWeapon_data._bulletPpref,
                                                        guntip.transform.position,
                                                        guntip.transform.rotation);

                    bullet_SG.transform.localScale = new Vector3(GameManager.instance.scaling * 0.06F,
                                                                GameManager.instance.scaling * 0.06F,
                                                                GameManager.instance.scaling * 0.06F);

                    bullet_SG.transform.eulerAngles = new Vector3(bullet_SG.transform.eulerAngles.x,
                                                                    bullet_SG.transform.eulerAngles.y,
                                                                    bullet_SG.transform.eulerAngles.z - 90);

                    Rigidbody2D rb = bullet_SG.GetComponent<Rigidbody2D>();
                    rb.AddForce(guntip.transform.up * 30f, ForceMode2D.Impulse);

                    GameManager.instance.fire = true;
                    recoil(GameManager.instance.scaling);
                    
                    Gun_sound_play(GameManager.instance.CurrentWeaponData.Code);

                    GameManager.instance.scaling = 0;
                    //Debug.Log("fire key up")
                    //Bryan
                    Ammunition -= 1;
                    StartCoroutine("FireRate", GameManager.instance.CurrentWeaponData.FireRateTime); 
                }
                else if(Input.GetButtonUp("Fire1"))
				{
                    audio1.Stop();
                }
                //transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 2, Time.deltaTime * 10);

            }

        }

    }
    private void SniperR_shoot()
    {
        if (CurrentWeapon_data.Code == "Item_WSR0_SniperR" && Input.GetKeyDown(KeyCode.Mouse0) && GameManager.instance.isPaused == false && Once)
        {
            //bullet.transform.localScale = new Vector3(1.27f, 1.27f, 1.27f);
            GameObject bullet_SR = Instantiate(CurrentWeapon_data._bulletPpref, guntip.transform.position, guntip.transform.rotation);
            bullet_SR.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            bullet_SR.transform.eulerAngles = new Vector3(bullet_SR.transform.eulerAngles.x, bullet_SR.transform.eulerAngles.y, bullet_SR.transform.eulerAngles.z - 90);
            Rigidbody2D rb = bullet_SR.GetComponent<Rigidbody2D>();
            rb.AddForce(guntip.transform.up * 30f, ForceMode2D.Impulse);

            //Debug.Log("AR shoot");
            //flip the image
            //GameObject.Find("PlayerTest").transform.GetChild(0).GetComponent<Transform>().rotation=Quaternion.Euler(0, 180, 0);

            GameManager.instance.fire = true;
            Gun_sound_play(GameManager.instance.CurrentWeaponData.Code);
            recoil(2f);
            StartCoroutine("FireRate", GameManager.instance.CurrentWeaponData.FireRateTime); //Bryan
        }
        else if (CurrentWeapon_data.Code == "Item_WSR1_SniperR" && Input.GetKeyDown(KeyCode.Mouse0) && GameManager.instance.isPaused == false && Once)
        {

            //bullet.transform.localScale = new Vector3(1.27f, 1.27f, 1.27f);
            GameObject bullet_SR = Instantiate(CurrentWeapon_data._bulletPpref, guntip.transform.position, guntip.transform.rotation);
            bullet_SR.transform.localScale = new Vector3(0.1f, 0.03f, 0.06f);
            bullet_SR.transform.eulerAngles = new Vector3(bullet_SR.transform.eulerAngles.x, bullet_SR.transform.eulerAngles.y, bullet_SR.transform.eulerAngles.z - 90);
            Rigidbody2D rb = bullet_SR.GetComponent<Rigidbody2D>();
            rb.AddForce(guntip.transform.up * 30f, ForceMode2D.Impulse);

            GameManager.instance.fire = true;
            Gun_sound_play(GameManager.instance.CurrentWeaponData.Code);
            recoil(2.5f);
            //Bryan
            Ammunition -= 1;
            StartCoroutine("FireRate", GameManager.instance.CurrentWeaponData.FireRateTime); 
        }
    }
    void Gun_sound_play(string WeaponCode)
    {
        audio0.clip = GameManager.instance.CurrentWeaponData._ItemSound0;
        Debug.Log("Gun sound play() run");

        if (WeaponCode == "Item_WP1_Pistol")
        {
            audio0.PlayOneShot(GameManager.instance.CurrentWeaponData._ItemSound0, 1f);
            //audio0.Play();
            audio1.clip = GameManager.instance.CurrentWeaponData._ItemSound1;
            audio1.PlayDelayed(0.2f);
            Debug.Log("WP1 gun sound play");
        }
        else if (WeaponCode == "Item_WAR1_AR")
        {
            audio0.PlayOneShot(GameManager.instance.CurrentWeaponData._ItemSound0);

        }
        else if (WeaponCode == "Item_WS1_Shotgun")
        {
            audio0.Play();
        }
        else //WP0, WAR0 ,WSR0, WS0
        {
            audio0.Play();
            //AudioSource.PlayClipAtPoint(CurrentWeapon_data._ItemSound0, this.gameObject.transform.position);
            Debug.Log("default sound played");
        }



    }
    private void Fire()
    {
        if (CurrentWeapon_data.Code.Contains("Pistol"))
        {
            Pistol_shoot();
            
        }
        else if (CurrentWeapon_data.Code.Contains("AR"))
        {
            AR_shoot();
        }
        else if (CurrentWeapon_data.Code.Contains("Shotgun"))
        {
            Shotgun_shoot();
        }
        else if (CurrentWeapon_data.Code.Contains("SniperR"))
        {
            SniperR_shoot();
        }
        
    }

    void UseActivatable()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.Activatables != null)
        {
            GameManager.instance.useItem(GameManager.instance.Activatables);
        }
        else if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.Activatables == null)
        {
            Debug.Log("Missing activatable");
        }
    }
    void recoil(float reC)
    {
        Vector2 Movement, Mouse_pos;
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 look_dir = Mouse_pos - Rb_player.position;
        if (Input.GetKey(KeyCode.Mouse0) && GameManager.instance.fire == true)
        {
            Movement.x = reC;
            Movement.y = reC;
            //Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
            Rb_player.MovePosition(Rb_player.position - Movement * GameManager.instance.PlayerSpeed * look_dir * 0.01f);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            GameManager.instance.fire = false;
        }
        //GameManager.instance.fire
    }




    private void Update()
    {
        //Chris ---- Start
        p_V3_cursor = checkCursor();
        _y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - player.transform.position.y;
        _x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - player.transform.position.x;
        angle = Mathf.Atan2(_y, _x) * Mathf.Rad2Deg;
        GunOrientation(gun_to_player_distance);
        Flip();
        //Chris ----- End

        if (Input.GetKeyDown(KeyCode.Mouse0) && Ammunition > 0)
        {
            Fire();
            TextAmmo.text = Ammunition + "/" + CurrentWeapon_data.MaxAmmo;
            if (Ammunition <= 0 )
            {
                 StartCoroutine("ReloadGun",GameManager.instance.CurrentWeaponData.ReloadTime);
            }
            
        }

        //recoil();
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            GameManager.instance.fire = false;
        }
        if (Input.GetButton("Fire1") && CurrentWeapon_data.Code == "Item_WS1_Shotgun" && GameManager.instance.scaling <= 2)
        {
            GameManager.instance.scaling += Time.deltaTime * 2;
            Debug.Log("scale: " + GameManager.instance.scaling);
        }
        UseActivatable();
    }
}
