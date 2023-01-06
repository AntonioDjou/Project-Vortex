using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OO_CheckPoint : MonoBehaviour 
{
	public static OO_CheckPoint checkpointScript;

	// Use this for initialization
	void Start () 
	{
		checkpointScript = this;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			other.GetComponent<OO_PlayerManager> ().V3_checkPointPosition = transform.position; //set the checkpoint position on the player to current 
																								//checkpoint position that the player hit
		}
	}
}
