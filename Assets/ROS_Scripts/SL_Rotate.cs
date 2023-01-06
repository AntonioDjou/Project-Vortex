using UnityEngine;
using System.Collections;

public class SL_Rotate : MonoBehaviour 
{
	
	//Memeber Variables
	public float FL_RotSpeed = 100f;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		RotateOtherObject(FL_RotSpeed);
	}

	void RotateOtherObject(float FL_RotSpeed)
	{
		transform.Rotate (0, 0, FL_RotSpeed * Time.deltaTime);
	}
}
