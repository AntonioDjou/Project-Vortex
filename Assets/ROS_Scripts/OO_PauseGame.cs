using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections.Generic;

public class OO_PauseGame : MonoBehaviour 
{
	
	public GameObject go_player;
	[Header("Button Audios")]
	[Space]

	public AudioClip aud_click;
	AudioSource aud_audSource;
	[Space]

	[Header("IsGamePaused")]
	[Space]
	public bool bl_pause = false;
	public bool bl_gameOver = false;

	[Header("UI Element")]
	[Space]
	public Canvas cV_pauseUI;
	public Canvas cV_confirmationPage;
	public Canvas cV_settingsCanvas;
	public Canvas cV_gameOverCanvas;
	[Space]

	[Header("Level to load when Exit Game")]
	[Space]
	public string stng_levelToLoad;


	//===========================================================================================================================

	void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
		aud_audSource = GetComponent<AudioSource> ();
	}

	//===========================================================================================================================

	void Update()
	{
		if(Input.GetKey (KeyCode.Escape))
		{
			bl_pause = true;
			Time.timeScale = 0;
			GameObject.Find ("PauseCanvas").GetComponent<Canvas> ().enabled = true;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		LoadGameOver (); //loading the GameOver Screen  
	}

	//=============================================================================================================================

	public void ResumeFromPause()
	{
		bl_pause = false;
		aud_audSource.PlayOneShot (aud_click);
		Time.timeScale = 1;
		GameObject.Find ("PauseCanvas").GetComponent<Canvas> ().enabled = false;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
	}

	//=============================================================================================================================

	public void LoadExitConfirmPage() // confirmation if player is exiting the game or returning to main menu
	{
		aud_audSource.PlayOneShot (aud_click);
		GameObject.Find ("PauseCanvas").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("ConfirmationCanvas").GetComponent<Canvas> ().enabled = true;
	}

	//=============================================================================================================================

	public void BackToPauseMenu()
	{
		aud_audSource.PlayOneShot (aud_click);
		GameObject.Find ("PauseCanvas").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("ConfirmationCanvas").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("SettingsCanvas").GetComponent<Canvas> ().enabled = false;
	}

	//=================================================================================================================================

	public void ExitGame()
	{
		aud_audSource.PlayOneShot (aud_click);
		SceneManager.LoadScene (stng_levelToLoad);
	}

	//================================================================================================================================

	public void LoadSettingsPageFromPause()
	{
		aud_audSource.PlayOneShot (aud_click);
		GameObject.Find ("PauseCanvas").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("SettingsCanvas").GetComponent<Canvas> ().enabled = true;
	}

	//===============================================================================================================================

	void LoadGameOver()
	{
		if(OO_Timer.timerScript.fl_countDownTimer <= 0)
		{
			bl_gameOver = true;
			GameObject.Find ("GameOverCanvas").GetComponent<Canvas> ().enabled = true;
		}
	}
		
	//=================================================================================================================================	
}
