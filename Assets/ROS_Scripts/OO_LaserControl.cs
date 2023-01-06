using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OO_LaserControl : MonoBehaviour 
{
	public Transform start_Pos;
	
	public Transform end_Pos;
	
	public Transform Moving_Obj;
	
	public Vector3 new_Pos;
	
	public string current_State;
	
	public float smooth;
	
	public float reset_Time;


	
	void Start()
	{
		SwopTarget();
	}
	
	void Update()
	{
		Moving_Obj.position = Vector3.Lerp (Moving_Obj.position, new_Pos, smooth * Time.deltaTime);
	}
	
	
	void SwopTarget()
	{
		if(current_State == "Moving To Start_Pos")
		{
			current_State = "Moving To end_Pos";
			new_Pos = end_Pos.position;
		}
		else if(current_State == "Moving To end_Pos")
		{
			current_State = "Moving To Start_Pos";
			new_Pos = start_Pos.position;
		}
		else if(current_State == "")
		{
			current_State = "Moving To end_Pos";
			new_Pos = end_Pos.position;
		}
		Invoke ("SwopTarget", reset_Time);
	}
		
}
