using System.Collections;
using System.Collections.Generic;
//using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float health = 10f;
	public int maxHealth = 30;
	[SerializeField]private float speed;
	[SerializeField]private float tilt;
	[SerializeField]private float fireRate;
	private float nextFire;
	private new Rigidbody rigidbody;
	private MeshRenderer playerRenderer;
	private MeshCollider playerCollider;
	private GameObject engineGO;
	private bool isAbleToAttack = false;
	
	[Space]
	[SerializeField]private GameObject shot;
	public Transform shotSpawn;
	[SerializeField]private GameObject playerExplosion;

	[Space]
	[SerializeField]private float padding = 2f;
	public float zMin;
	public float zMax;
	public float xMin;
	public float xMax;

	private float moveHorizontal;
	private float moveVertical;

	// Gamepad Implementation
	private bool joystickController = false;
    private bool leftJoystick = false;

	void Start()
	{
		engineGO = GameObject.Find("engines_player");
		playerRenderer = GetComponent<MeshRenderer>();
		playerCollider = GetComponent<MeshCollider>();
		rigidbody = GetComponent<Rigidbody>();
		cameraViewRestriction();
	}

	void Update()
	{
		if(!isAbleToAttack)
		{
			if((Input.GetKeyDown(KeyCode.Space) || Input.GetButton("AButton") && Time.time > nextFire))
			{
				nextFire = Time.time + fireRate;
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				health -= 1;
				if(health <= 0)
				{
					StartCoroutine(Die(3f));
				}
			}
		}
	}

	void FixedUpdate()
	{
		if(!isAbleToAttack)
		{
			if(!joystickController)
			{
				moveHorizontal = Input.GetAxis("Horizontal");
				moveVertical = Input.GetAxis("Vertical");
			}
			else
			{
				// Gather input from joystick controller
				if(leftJoystick)
				{
					moveHorizontal = Input.GetAxis("LeftJoystickHorizontal");
					moveVertical = Input.GetAxis("LeftJoystickVertical"); 
				}
				else if(!leftJoystick)
				{
					moveHorizontal = Input.GetAxis("RightJoystickHorizontal");
					moveVertical = Input.GetAxis("RightJoystickVertical");
				}
				else
				{
					Debug.LogError("Error: Trying to get input controller without any button pressed");
				}
			}
		}

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		float newX = Mathf.Clamp(rigidbody.position.x, xMin, xMax);
		float newZ = Mathf.Clamp(rigidbody.position.z, zMin, zMax);
		rigidbody.position = new Vector3(newX, 0.0f, newZ);
		
		rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}
	
	public void cameraViewRestriction()
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
	}

	void OnTriggerEnter(Collider other)
	{
		Battery battery = other.gameObject.GetComponent<Battery>();
		if(other.CompareTag("Battery"))
		{
			if(health < maxHealth)
			{
				health += battery.recharge();
			}
			battery.Hit();
			Destroy(other.gameObject);
		}

		Laser missile = other.gameObject.GetComponent<Laser>();
		if(missile)
		{
			Debug.Log("Player collided with a missile");
			health -= missile.GetDamage();
			missile.Hit();
			if(health <= 0)
			{
				StartCoroutine(Die(3f));
			}
		}

		if((other.CompareTag("Enemy")) || (other.CompareTag("Asteroid")) )
		{
			Destroy(other.gameObject);
			StartCoroutine(Die(3f));
		}
	}

	public IEnumerator Die(float seconds)
	{
		Instantiate(playerExplosion, transform.position, transform.rotation);
		isAbleToAttack = true;
		engineGO.SetActive(false);
		Debug.Log("engineGO condition: "+ engineGO.activeInHierarchy);
		playerRenderer.enabled = false;
		playerCollider.enabled = false;
		yield return new WaitForSeconds(seconds);

		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
		Destroy(gameObject);
	}
}
