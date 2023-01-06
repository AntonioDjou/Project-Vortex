using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OO_MainMenu : MonoBehaviour 
{
	[Header("Menu UI Sound")]
	[Space]

	public AudioClip aud_click;
	AudioSource aud_audioSource;
	[Space]

	public static OO_MainMenu menuScript;
	[Space]

	[Header("MenuCanvas")]
	[Space]

	public Canvas cV_menuCanvas;
	public Canvas cV_multiplayerCanvas;
	public Canvas cV_controlCanvas;
	public Canvas cV_SettingsCanvas;
	public Canvas cV_CreditsCanvas;
	[Space]

	[Header("GameOBject Components in Canvas")]
	[Space]

	public GameObject go_ctrllerIcon;
	public GameObject go_keyBoardCtrlIcon;

	[Space]

	[Header("Level To Load")]
	[Space]

	public string stng_loadLevel;

	//===================================================================================================================================
	void Start () 
	{
		menuScript = this;
		aud_audioSource = GetComponent<AudioSource> ();
    }
	//======================================================================================================================================

	void Update () 
	{
	}
	//=========================================================================================================================================

	public void StartGame()
	{
		SceneManager.LoadScene (stng_loadLevel);
		Debug.Log ("Loading Tutorial");
		aud_audioSource.PlayOneShot (aud_click); //play click buttn audio
		GameObject.Find ("MenuCanvas").GetComponent<Canvas> ().enabled = false;
	}
	//=========================================================================================================================================

	public void ExitGame()
	{
		Application.Quit ();
	}

	//===========================================================================================================================================

	public void RestartGame()
	{
		aud_audioSource.PlayOneShot (aud_click); //play click buttn audio
		SceneManager.LoadScene (stng_loadLevel);
	}
	//===========================================================================================================================================

	public void BackToMenu()
	{
		aud_audioSource.PlayOneShot (aud_click); //play click buttn audio
		GameObject.Find ("MultiplayerCanvas").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("MenuCanvas").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("ControlCanvas").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("SettingsCanvas").GetComponent<Canvas> ().enabled = false;
	}
	//============================================================================================================================================

	public void LoadMultiplayer()
	{
		aud_audioSource.PlayOneShot (aud_click); //play click buttn audio
		GameObject.Find ("MenuCanvas").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("MultiplayerCanvas").GetComponent<Canvas> ().enabled = true;
	}
	//================================================================================================================================================

	public void LoadControlPage()
	{
		aud_audioSource.PlayOneShot (aud_click); //play click buttn audio
		GameObject.Find ("ControlCanvas").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("MenuCanvas").GetComponent<Canvas> ().enabled = false;
		go_ctrllerIcon.SetActive (false);
	}

	//================================================================================================================================================

	public void EnableKeyBoardControl()
	{
		aud_audioSource.PlayOneShot (aud_click); //play click buttn audio
		go_keyBoardCtrlIcon.SetActive (true);
		go_ctrllerIcon.SetActive (false);
	}
	//=================================================================================================================================================

	public void EnableContollerControl()
	{
		aud_audioSource.PlayOneShot (aud_click); //play click buttn audio
		go_keyBoardCtrlIcon.SetActive (false);
		go_ctrllerIcon.SetActive (true);
	}
	//====================================================================================================================================================

	public void LoadSettingsPage()
	{
		aud_audioSource.PlayOneShot (aud_click);
		GameObject.Find ("SettingsCanvas").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("MenuCanvas").GetComponent<Canvas> ().enabled = false;
	}
	//==================================================================================================================================================

	public void LoadCreditPage()
	{
		aud_audioSource.PlayOneShot (aud_click);
		GameObject.Find ("CreditsCanvas").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("SettingsCanvas").GetComponent<Canvas> ().enabled = false;
	}
	//=================================================================================================================================================

	public void ExitCreditPage()
	{
		aud_audioSource.PlayOneShot (aud_click);
		GameObject.Find ("CreditsCanvas").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("SettingsCanvas").GetComponent<Canvas> ().enabled = true;
	}
		
}
