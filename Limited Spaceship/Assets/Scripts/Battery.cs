using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Battery : MonoBehaviour 
{
	[SerializeField]private GameObject particles;
	private new Rigidbody rigidbody;
	public int energyLife = 5;
	[SerializeField]private float speed;
	public int scoreValue = 50;
	private ScoreKeeper scoreKeeper;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();

		rigidbody.velocity = transform.forward * speed;
	}

	public int recharge()
	{
		return energyLife;
	}
	
	public void Hit()
	{
		Instantiate(particles, transform.position, transform.rotation);
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			if(particles != null)
			{
				Instantiate(particles, other.transform.position, other.transform.rotation);
				Destroy(gameObject);
			}
		}
		if(other.CompareTag("Shredder") || other.CompareTag("Enemy") || other.CompareTag("Laser"))
		{
			return;
		}
		
		scoreKeeper.Score(scoreValue);
	}
}
