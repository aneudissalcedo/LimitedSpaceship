using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Evasion : MonoBehaviour
{
	[SerializeField]private Vector2 startWait;
	[SerializeField]private Vector2 evasionTime;
	[SerializeField]private Vector2 evasionWait;
	[SerializeField]private float dodge;
	[SerializeField]private float smoothing;
	[SerializeField]private float tilt;
	
	[Space]
	private float targetEvasion;
	private float currentSpeed;
	private new Rigidbody rigidbody;
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		currentSpeed = rigidbody.velocity.z;
		StartCoroutine(Evade());
	}

	IEnumerator Evade()
	{
		yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
		
		while(true)
		{
			targetEvasion = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds(Random.Range(evasionTime.x, evasionTime.y));
			targetEvasion = 0;
			yield return new WaitForSeconds(Random.Range(evasionWait.x, evasionWait.y));
		}
	}

	void FixedUpdate()
	{
		float newEvasion = Mathf.MoveTowards(rigidbody.velocity.x, targetEvasion, Time.deltaTime * smoothing);
		rigidbody.velocity = new Vector3(newEvasion, 0.0f, currentSpeed);
		
		rigidbody.rotation =Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}
}
