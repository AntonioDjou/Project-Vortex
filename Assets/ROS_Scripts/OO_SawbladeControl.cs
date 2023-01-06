using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OO_SawbladeControl : MonoBehaviour 
{

	[Header ("Unity Setup Field")]

	public Transform tr_StartPos;
	public Transform tr_EndPos;
	public float fl_rotateSpeed = 20f;
	public float fl_moveSpeed = 15;
	bool bl_SwitchPosition = false;


	void Start()
	{
	}
		
	// Update is called once per frame
	void FixedUpdate () 
	{
		//Rotate the gameobject on Vector3 using real time * the rotation speed 
		transform.Rotate(Vector3.up * Time.deltaTime* fl_rotateSpeed);

		//setting up the position of the object
		// if the position is the end position, switch position is true
		if(transform.position == tr_EndPos.position)
		{
			bl_SwitchPosition = true;
		}

		//setting up the position of the object
		// if the position is the start position, switch position is false
		if(transform.position == tr_StartPos.position)
		{
			bl_SwitchPosition = false;
		}

		//if switchposition is true set the movement of the gameobject to the start position
		if(bl_SwitchPosition)
		{
			transform.position = Vector3.MoveTowards (transform.position, tr_StartPos.position, fl_moveSpeed);
		}

		//otherwise if switchposition is false set the movement of the gameobject to the end position
		else
		{
			transform.position = Vector3.MoveTowards (transform.position, tr_EndPos.position, fl_moveSpeed);
		}

	}
}
