using System.Collections;
using System.Collections.Generic;
//using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float health = 10f;
	[SerializeField]private float speed;
	[SerializeField]private float tilt;
	[SerializeField]private float fireRate;
	private float nextFire;
	private new Rigidbody rigidbody;
	
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

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		cameraViewRestriction();
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.Space) && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			health -= 1;
			if(health <= 0)
			{
				Die();
			}
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

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
			if(health < 30)
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
				Die();
			}
		}

		if((other.CompareTag("Enemy")) || (other.CompareTag("Asteroid")) )
		{
			Instantiate(playerExplosion, transform.position, transform.rotation);
			LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
			man.LoadLevel("Win Screen");
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}

	public void Die()
	{
		Instantiate(playerExplosion, transform.position, transform.rotation);
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
		Destroy(gameObject);

		//LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		//man.LoadLevel("Win Screen");

		//SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}
}
