using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	[SerializeField]private GameObject[] hazards;
	[SerializeField]private Vector3 spawnValues;
	[SerializeField]private int hazardCount;
	[SerializeField]private float spawnWait;
	[SerializeField]private float startWait;
	[SerializeField]private float waveWait;
	public PlayerController playerController;

	void Start()
	{
		StartCoroutine(SpawnWaves());
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while(true)
		{
			for(int i=0; i<hazardCount; i++)
			{
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				Vector3 spawnPosition = new Vector3(Random.Range
					(playerController.xMin, playerController.xMax), spawnValues.y, spawnValues.z);
				
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}
}
