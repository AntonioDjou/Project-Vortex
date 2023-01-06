using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OO_Camera : MonoBehaviour 
{
	[Header("Camera Attributes")]
	[Space]

	public float smooth = 3f; // a public variable to adjust smoothing of camera motion
	Transform standardPos; // the usual position for the camera, specified by a transform in the game
	Transform lookAtPos; // the position to move the camera to when using head look
	
	void Start()
	{
		// initialising references
		standardPos = GameObject.Find ("Camera").transform;
		
		if(GameObject.Find ("CamLookAtPos"))
			lookAtPos = GameObject.Find ("CamLookAtPos").transform;
	}
	
	void FixedUpdate ()
	{
		// if we hold Alt
		if(Input.GetButton("Fire2") && lookAtPos)
		{
			// lerp the camera position to the look at position, and lerp its forward direction to match 
			transform.position = Vector3.Lerp(transform.position, lookAtPos.position, Time.deltaTime * smooth);
			transform.forward = Vector3.Lerp(transform.forward, lookAtPos.forward, Time.deltaTime * smooth);
		}
		else
		{	
			// return the camera to standard position and direction
			transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime * smooth);	
			transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.deltaTime * smooth);
		}
	}
}
