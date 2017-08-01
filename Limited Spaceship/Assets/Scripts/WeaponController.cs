using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponController : MonoBehaviour 
{
	[SerializeField]private GameObject shot;
	[SerializeField]private Transform shotSpawn;
	[SerializeField]private float fireRate;
	[SerializeField]private float delay;

	void Start()
	{
		InvokeRepeating("Fire", delay, fireRate);
	}
	
	void Fire()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
	}
}
