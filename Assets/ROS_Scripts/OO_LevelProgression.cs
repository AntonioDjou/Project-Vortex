using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
[RequireComponent(typeof(AudioSource))]

public class OO_LevelProgression : MonoBehaviour 
{
	public static OO_LevelProgression lvlprogressScript;

	[Header("Variables")]
	[Space]
	public bool bl_levelAchieved = false;

	[Header("Level To Load When Select Next Level")]
	[Space]

	public string stng_loadLevel;

	[Header("Level Achievement UI")]
	[Space]
	public GameObject go_lvlAchieveUI;


	//===============================================================================================================

	void Start()
	{
	}

	//=================================================================================================================

	void Update()
	{
	}

	void OnTriggerEnter(Collider player)
	{
		if(player.CompareTag ("Player"))
		{
			bl_levelAchieved = true;
			if(bl_levelAchieved)
			{
				GameObject.Find ("AchievementCanvas").GetComponent<Canvas> ().enabled = true;
				Time.timeScale = 0;
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
			}
		}
	}

	//==================================================================================================================

	public void SelectNextLevel()
	{
		SceneManager.LoadScene (stng_loadLevel);
	}

	public void ReplayLevel()
	{
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene (scene.name); 
	}
}
