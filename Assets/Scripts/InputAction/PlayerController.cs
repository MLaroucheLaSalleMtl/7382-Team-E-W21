using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerController controls;
	// Start is called before the first frame update
	private void OnEnable()
	{
        controls.OnEnable();
	}
	private void OnDisable()
	{
		controls.OnDisable();
	}
	private void Awake()
	{
        controls = new PlayerController();
	}
	void Start()
    {
		//controls.Player.Shoot.performed += _ => PlayerShoot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void Shoot()
	{
		//Vector2 2 mousePosition= controls.Player.MousePosition.
	}
}
