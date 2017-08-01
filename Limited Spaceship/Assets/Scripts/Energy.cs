using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Energy : MonoBehaviour 
{
	public PlayerController playerController;
	private Slider slider;

	void Start () 
	{
		slider = GetComponent<Slider>();
	}

	void Update () 
	{
		slider.value = playerController.health;
	}
}
