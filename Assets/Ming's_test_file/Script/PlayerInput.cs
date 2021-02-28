using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
	//Classes are missing: Weapon, Player, Item


	public Camera Cam;
	public Rigidbody2D Rb_player;
	//public Transform firePoint;

	//string Current_weapon="Pistol";
	string Current_weapon = "Shotgun";
	//public string Current_weapon = "AR";
	//string Current_weapon = "SniperR";
	bool fire = false;

	public float MovementSpeed = 5f, BulletSpeed = 20f;
	public Vector2 Movement, Mouse_pos;

	[SerializeField] private Transform guntip;
	[SerializeField] private GameObject player, collector, item_prefab, gadget_prefab, bullet_prefab;
	[SerializeField] private UI_inventory uiInventory;
	[SerializeField] private GameManager gameManager;
	//[SerializeField] private GameObject uiInventory_gameobject;
	//Inventory inventory;

	void Start()
    {
        
    }

    private void Awake()
    {
		//inventory = new Inventory();
		//uiInventory.SetInventory(inventory);
		//ItemSpawn.SpwanItem(new Vector3(0, 0), new Item { itemType = Item.ItemType.Pistol, amount = 1 });
		//ItemSpawn.SpwanItem(new Vector3(10, 0), new Item { itemType = Item.ItemType.HealingPill, amount = 1 });
		//ItemSpawn.SpwanItem(new Vector3(-10, 0), new Item { itemType = Item.ItemType.BoostSyringe, amount = 1 });
	}
    // Update is called once per frame
    void Update()
    {
		//Movement();
		Movement.x = Input.GetAxisRaw("Horizontal");
		Movement.y = Input.GetAxisRaw("Vertical");
		Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
		Rb_player.MovePosition(Rb_player.position + Movement * MovementSpeed * Time.fixedDeltaTime);

		//Mouse tracking
		Vector2 look_dir = Mouse_pos - Rb_player.position;
		float angle = Mathf.Atan2(look_dir.y, look_dir.x) * Mathf.Rad2Deg - 90f;
		Rb_player.rotation = angle;



		
		shoot(Current_weapon);
		if(fire)
        {
			if(Current_weapon== "Pistol")
            {
				Movement.x = 0.05f;
				Movement.y = 0.05f;
				Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
				Rb_player.MovePosition(Rb_player.position - Movement * MovementSpeed * look_dir * 0.01f);
			}
			else if(Current_weapon=="Shotgun")
            {
				Movement.x = 0.2f;
				Movement.y = 0.2f;
				Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
				Rb_player.MovePosition(Rb_player.position - Movement * MovementSpeed * look_dir * 0.01f);
			}
			else if (Current_weapon == "AR")
			{
				Movement.x = 0.8f;
				Movement.y = 0.8f;
				Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
				Rb_player.MovePosition(Rb_player.position - Movement * MovementSpeed * look_dir * 0.01f);
			}
			else if (Current_weapon == "SniperR")
			{
				Movement.x = 2f;
				Movement.y = 2f;
				Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
				Rb_player.MovePosition(Rb_player.position - Movement * MovementSpeed * look_dir * 0.01f);
			}


		}
		if(Input.GetKeyUp(KeyCode.Mouse0))
        {
			fire = false;
        }

		ChangeWeapon();

		DropItem(player);


		Dash(player, look_dir);




		Throw(look_dir);
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Item")
		{
			//gameManager.AddItem()£»
			//instantisate pick effect here
			Destroy(gameObject);
		}
	}

	private void Move()//Character player.vector3
    {
        Movement.x = Input.GetAxis("Honrizontal");
		Movement.y = Input.GetAxis("Vertical");
		Mouse_pos = Cam.ScreenToWorldPoint(Input.mousePosition);
		Rb_player.MovePosition(Rb_player.position + Movement * MovementSpeed * Time.deltaTime);
        //transform.position = new Vector2(movement, 0, 0) * Time.deltaTime * MovementSpeed;
    }
	void shoot(string weapon_name)
    {
		//Vector2 look_dir = Mouse_pos - Rb_player.position;
		if (weapon_name=="Pistol"&Input.GetKeyDown(KeyCode.Mouse0)& GameManager.instance.isPaused == false)
        {
			GameObject bullet_pistol = Instantiate(bullet_prefab, guntip.position, guntip.rotation);
			Rigidbody2D rb = bullet_pistol.GetComponent<Rigidbody2D>();
			rb.AddForce(guntip.up * BulletSpeed, ForceMode2D.Impulse);
			fire = true;
			Debug.Log("pistol shoot");
		}
		else if(weapon_name=="Shotgun"&Input.GetKeyDown(KeyCode.Mouse0) & GameManager.instance.isPaused == false)
        {
			/*Vector2 lookDir = mousePos - rb.position;
			float aiming_line = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
			float start_line = aiming_line + 30f;
			float angle_increase = 10f;*/
			for (int i =0;i<=6;i++)
            {
				

				bullet_prefab.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
				GameObject bullet_shotgun = Instantiate(bullet_prefab, guntip.position, guntip.rotation);
				//GameObject bullet_shotgun = Instantiate(bulletPrefab, Guntip.position,Guntip.rotation);
				Rigidbody2D rb_shotgun = bullet_shotgun.GetComponent<Rigidbody2D>();
				switch (i)
				{
					case 0:
						rb_shotgun.AddForce(guntip.up * BulletSpeed + new Vector3(5f, 8.7f, 0f), ForceMode2D.Impulse);
						//Debug.Log("shotgun case0");
						break;
					case 1:
						rb_shotgun.AddForce(guntip.up * BulletSpeed + new Vector3(7.7f, 6.4f, 0f), ForceMode2D.Impulse);
						//Debug.Log("shotgun case1");
						break;
					case 2:
						rb_shotgun.AddForce(guntip.up * BulletSpeed + new Vector3(9.4f, 3.4f, 0f), ForceMode2D.Impulse);
						//Debug.Log("shotgun case2");
						break;
					case 3:
						rb_shotgun.AddForce(guntip.up * BulletSpeed + new Vector3(0f, 0f, 0f), ForceMode2D.Impulse);
						//Debug.Log("shotgun case3");
						break;
					case 4:
						rb_shotgun.AddForce(guntip.up * BulletSpeed + new Vector3(9.4f, -3.4f, 0f), ForceMode2D.Impulse);
						//Debug.Log("shotgun case4");
						break;
					case 5:
						rb_shotgun.AddForce(guntip.up * BulletSpeed + new Vector3(7.7f, -6.4f, 0f), ForceMode2D.Impulse);
						//Debug.Log("shotgun case5");
						break;
					case 6:
						rb_shotgun.AddForce(guntip.up * BulletSpeed + new Vector3(5f, -8.7f, 0f), ForceMode2D.Impulse);
						//Debug.Log("shotgun case6");
						break;
				}

				//rb.AddForce(Guntip.up * bulletSpeed*0.000001f + new Vector3(0f, 90, 0f), ForceMode2D.Impulse);
				/**/
				//Debug.Log("shotgun shoot");
			}
			fire = true;
			
		}
		else if(weapon_name=="AR"&Input.GetKey(KeyCode.Mouse0) & GameManager.instance.isPaused == false)
        {
			bullet_prefab.transform.localScale = new Vector3(0.556f, 0.556f, 0.556f);
			GameObject bullet_AR = Instantiate(bullet_prefab, guntip.position, guntip.rotation);
			Rigidbody2D rb = bullet_AR.GetComponent<Rigidbody2D>();
			rb.AddForce(guntip.up * BulletSpeed, ForceMode2D.Impulse);
			fire = true;
			Debug.Log("AR shoot");
		}
		else if (weapon_name=="SniperR" & Input.GetKeyDown(KeyCode.Mouse0) & GameManager.instance.isPaused == false)
        {
			bullet_prefab.transform.localScale = new Vector3(1.27f, 1.27f, 1.27f);
			GameObject bullet_SR = Instantiate(bullet_prefab, guntip.position, guntip.rotation);
			Rigidbody2D rb = bullet_SR.GetComponent<Rigidbody2D>();
			rb.AddForce(guntip.up * BulletSpeed, ForceMode2D.Impulse);
			fire = true;
			Debug.Log("AR shoot");
		}
	}
	private void DropItem(GameObject player)
	{
		//if (Input.GetKey(KeyCode.T))// & player.item_on_hold != null)
		//{
		//player.item_on_hold = null;
		//Transform pos = player.transform;
		//Vector3 pos = player.GetComponent<Transform>().position;
		if (Input.GetKeyUp(KeyCode.T))
		{
			GameObject droped_item = Instantiate(item_prefab, player.GetComponent<Transform>().position, player.transform.rotation);

		}
		

		//}
	}

	private void Dash(GameObject player,Vector2 lookDir)
	{
		//if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))// & player.DashTF = true)
		//{
			//Vector2 player_postion = Player.transform.position;
		float x = lookDir.x;
		float y = lookDir.y;
		//movement.x = Input.GetAxis("Honrizontal");
		//movement.y = Input.GetAxis("Vertical");
		while(x*x+y*y>32f)
        {
			x = x * 0.8f;
			y = y * 0.8f;
        }
		//player.GetComponent<Transform>().position = new Vector2( lookDir.x + 0.02f, lookDir.y + 0.02f);//(player_postion.x + 0.09f, player_postion.y + 0.09f);
		//player.DashCD = player.DashCD_ThreshHold;
		//}

		if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift) & GameManager.instance.isPaused == false)
		{
			player.GetComponent<Transform>().position = new Vector2(player.transform.position.x + x, player.transform.position.y + y);
		}
	}
	private void Throw(Vector2 lookdir)//GameObject player,)
	{
		//if (Input.GetKeyUp(KeyCode.R))// & player.gadget != null & player.gadgetCD = true)
		//{
			//player.UseGadget();//<----,player.gadgetCD = x;
			
		if (Input.GetKeyUp(KeyCode.R) & GameManager.instance.isPaused == false)
		{
			//firePoint = Guntip.transform;
			GameObject gadget = Instantiate(gadget_prefab, guntip.position, guntip.rotation);
			Rigidbody2D rb = gadget.GetComponent<Rigidbody2D>();
			rb.AddForce(guntip.up * BulletSpeed * 0.6f, ForceMode2D.Impulse);//velocity

		}
		//}
	}


	void ChangeWeapon()
    {
		if(Input.GetKeyDown(KeyCode.Alpha1) & GameManager.instance.isPaused == false)
        {
			Current_weapon = "Pistol";
			Debug.Log("Weapon change to pistol");
        }
		else if(Input.GetKeyDown(KeyCode.Alpha2) & GameManager.instance.isPaused == false)
        {
			Current_weapon = "AR";
        }
		else if (Input.GetKeyDown(KeyCode.Alpha3) & GameManager.instance.isPaused == false)
		{
			Current_weapon = "SniperR";
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4) & GameManager.instance.isPaused == false)
		{
			Current_weapon = "Shotgun";
		}
	}
















	/*private void Shoot(Weapon weapon) //load the property of the character's on-hold weapon(range, fire mode, dmg...) 
	{
		RaycastHit hit;
		Vector2 mous_pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		Vector2 pivot = new Vector2(Pivot.transform.position.x, Pivot.transform.position.y);
		Vector2 dir = mous_pos - pivot;
		if (Input.GetKey(KeyCode.Mouse0) & weapon.mode=="pistol")
        {
			GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
			Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
			rb.AddForce(firePoint.up * weapon.bulletSpeed, ForceMode2D.Impulse);
			if (Physics.Raycast(mous_pos, dir, out hit, weapon.range))
			{
				Debug.Log(hit.transform.name);
				if (hit.collider.gameObject.tag == "Creature") 
                {
					hit.collider.gameObject.TakeDmg();
                }
			}
		}
		else if(Input.GetKey(KeyCode.Mouse0)&weapon.mode == "rifle")
		{ 
		}
		else if(Input.GetKey(KeyCode.Mouse0)&weapon.mode == "shotgun")
        {

        }	
		else if(Input.GetKey(KeyCode.Mouse0)&weapon.mode == "sniper")
        {

        }
		
	}
	private void UseItem(Item item)
	{
		if (Input.GetKey(KeyCode.R) & item != null)//KeyCode == "mousebottom1" )
		{
			item.Use();
			
		}
	}

	private void OnCollisionEnter2D(Collision2D col)//
	{
		if (Input.GetKey(KeyCode.E)&col.gameObject.tag == "weapon")//
		{
			UpdateWeapon(col.gameObject);
		}
		else if (Input.GetKey(KeyCode.E) & col.gameObject.tag == "consumable")//
		{
			UpdateConsumable(col.gameObject);
		}
		else if (Input.GetKey(KeyCode.E) & col.gameObject.tag == "equiment")//
		{
			UpdateEquiment(col.gameObject);
		}
	}
	private void UpdateConsumable(GameObject item)
	{
		player.item = item;
	}
	private void UpdateWeapon(GameObject weapon)
    {
		player.weapon = weapon;
    }
	private void UpdateEquiment(GameObject equiment)
	{
		player.equimentAdd();
	}
	private void Dash(GameObject player)
	{
		if (Input.GetKey(KeyCode.LeftShift)|| Input.GetKey(KeyCode.RightShift)&player.DashTF=true)
		{
			Vector2 player_postion = Player.transform.position;
			float x = player_postion.x;
			float y = player_postion.y;

			player.GetComponent<Transform>().position = new Vector2(player_postion.x+50f, player_postion.y + 50f);
			player.DashCD = player.DashCD_ThreshHold;
		}
	}
	private void DropItem(GameObject player)
	{
		if (Input.GetKey(KeyCode.T)& player.item_on_hold != null)
		{
			player.item_on_hold = null;
			Transform pos = player.transform;
			GameObject droped_item = Instantiate(itemPrefab,pos);
		}
	}
	private void Throw(GameObject player)
	{
		if (Input.GetKey(KeyCode.R) & player.gadget != null&player.gadgetCD=true)
		{
			player.UseGadget();//<----,player.gadgetCD = x;
		}
	}
	*/

}
