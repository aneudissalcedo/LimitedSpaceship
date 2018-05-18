using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnergyText : MonoBehaviour 
{
	public PlayerController playerController;
	private Text energyText;

	void Start()
	{
		energyText = GetComponent<Text>();
	}

	void Update()
	{
		if(playerController.health > playerController.maxHealth)
		{
			playerController.health = playerController.maxHealth;
			energyText.text = playerController.health.ToString() +" / "+ playerController.maxHealth.ToString();	
		}
		energyText.text = playerController.health.ToString() +" / "+ playerController.maxHealth.ToString();
	}
}
