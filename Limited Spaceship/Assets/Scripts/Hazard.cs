using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hazard : MonoBehaviour 
{
	[SerializeField]private float tumble;
	[SerializeField]private float speed;
	public int scoreValue = 50;
	private ScoreKeeper scoreKeeper;
	[Space]
	[SerializeField]private GameObject explosion;
	[SerializeField]private GameObject playerExplosion;
	private new Rigidbody rigidbody;
	private PlayerController _player;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();

		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
		rigidbody.velocity = transform.forward * speed;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Laser"))
		{
			other.gameObject.GetComponent<Laser>().Hit();
			if(explosion != null)
			{
				Instantiate(explosion, transform.position, transform.rotation);
			}
		}
		// if(other.CompareTag("Player"))
		// {
		// 	Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
		// }
		if(
			other.CompareTag("Shredder") 
			|| other.CompareTag("Enemy")
			|| other.CompareTag("EnemyLaser")
			|| other.CompareTag("Asteroid")
		)
		{
			return;
		}
		//Destroy(other.gameObject);
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
	}
}
