using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OO_CombatRestriction : MonoBehaviour 
{
	//public static OO_CombatRestriction combatrestrictionScript;
	[Header("Attributes")]
	[Space]
	public GameObject go_player;

	[Header("Variables")]
	[Space]
	public float fl_playerDistanceToCombatArea = 15f;



	//==========================================================================================================

	void Start () 
	{
	}
	
	//===========================================================================================================

	void Update () 
	{
		float fl_distanceToPlayer = Vector3.Distance (go_player.transform.position, transform.position); //calculate the distance of combat area to the player

		if(fl_distanceToPlayer <= fl_playerDistanceToCombatArea) // check if the distance is less than value set
		{
			go_player.GetComponent<Animator> ().SetBool ("Jump", false);
			go_player.GetComponent<Animator> ().SetBool ("RollLeft", false);
			go_player.GetComponent<Animator> ().SetBool ("RollRight", false);
			go_player.GetComponent<Animator> ().SetBool ("RollBack", false);
			go_player.GetComponent<Animator> ().SetBool ("Slide", false);
			go_player.GetComponent<Animator> ().SetBool ("SprintLeft", false);
			go_player.GetComponent<Animator> ().SetBool ("SprintRight", false);

			Debug.Log ("No Animation performed");
		}
	}

	//===================================================================================================================
}
