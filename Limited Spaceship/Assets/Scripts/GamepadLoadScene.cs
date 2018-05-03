using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GamepadLoadScene : MonoBehaviour 
{
	void Update()
	{
		GetScene();
	}
	public void GetScene()
	{
		string activeScene = (string) SceneManager.GetActiveScene().name;
		string startMenuScene = (string) SceneManager.GetSceneByName("Start Menu").name;
		string winScreenScene = (string) SceneManager.GetSceneByName("Win Screen").name;
		Debug.Log("Active Scene: "+ activeScene);
		Debug.Log("Destination Scene: "+ startMenuScene);
		Debug.Log("Destination Scene: "+ winScreenScene);
		
		if(activeScene == startMenuScene && Input.GetButton("AButton"))
		{
			SceneManager.LoadScene("Game");
			Debug.Log("Game Scene Requested from: "+ activeScene +" Scene");
		}
		else if(activeScene == startMenuScene && Input.GetButton("XButton"))
		{
			Application.Quit();
			Debug.Log("Quit Requested from: "+ activeScene +" Scene");
		}
		else if(activeScene == winScreenScene && Input.GetButton("AButton"))
		{
			SceneManager.LoadScene("Game");
			Debug.Log("Game Scene Requested from: "+ activeScene +" Scene");
		}
		else if(activeScene == winScreenScene && Input.GetButton("XButton"))
		{
			Application.Quit();
			Debug.Log("Quit Requested from: "+ activeScene +" Scene");
		}
	}	
}
