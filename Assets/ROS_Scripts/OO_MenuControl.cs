using UnityEngine;
using System.Collections;

public class OO_MenuControl : MonoBehaviour 
{
	public Texture2D Buttons;
	public Texture2D hover;
	

	public string app_loadNextLevel;


	void Start()
	{
		Buttons = null;

	}

	void OnMouseEnter()
	{
		GetComponent<GUITexture>().texture = Buttons;
	}
	void OnMouseExit()
	{
		GetComponent<GUITexture>().texture = hover;

	}
	

	void OnMouseDown()
	{
		Application.LoadLevel (app_loadNextLevel);
	}
	
}
