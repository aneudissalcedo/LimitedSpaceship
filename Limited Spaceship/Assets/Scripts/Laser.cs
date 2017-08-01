using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour 
{
	[SerializeField]private float speed;
	private new Rigidbody rigidbody;

	[Space]
	public float damage = 1f;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.velocity = transform.forward * speed;
	}

	public float GetDamage()
	{
		return damage;
	}

	public void Hit()
	{
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Battery"))
		{
			return;
		}
	}
}
