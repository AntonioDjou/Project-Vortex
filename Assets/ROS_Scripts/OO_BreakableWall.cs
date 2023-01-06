using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OO_BreakableWall : MonoBehaviour 
{
	public GameObject go_breakParticle;
	public float fl_playerHealth = 20f;

	// Use this for initialization
	void Start () 
	{
		//breakablewallScript = this;
		go_breakParticle.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			Destroy (gameObject, 1f);
			go_breakParticle.SetActive (true);
			if(OO_PlayerManager.playerManagerScript.bl_canBreakWall == false)
			{
				if(!OO_PlayerManager.playerManagerScript.bl_activateShield)
				{
					OO_PlayerManager.playerManagerScript.fl_minHealth -= fl_playerHealth;
				}
			}
		}
	}
}
