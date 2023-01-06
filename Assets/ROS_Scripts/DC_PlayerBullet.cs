using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC_PlayerBullet : MonoBehaviour 
{
	GameObject player;

	void Start () 
	{	
		player = GameObject.Find("Exo_Gray");
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Enemy")
		{
			if(other.gameObject.GetComponent<OO_EnemyScript>() != null) // when you shoot the robot
			{
				GetComponent<SphereCollider>().enabled = false; // turn off collider
				other.gameObject.GetComponent<OO_EnemyScript>().fl_minhealth -= 25;

				if(player.GetComponent<OO_PlayerManager>().enemiesToKill >= 1 && other.gameObject.GetComponent<OO_EnemyScript>().fl_minhealth <= 0) // if enemy has 0 health
				{
					Destroy(other.gameObject); // destroy the enemy after health has reached 0
					player.GetComponent<OO_PlayerManager>().enemiesToKill -= 1; // check for the number of enemies and subtract 1 from the number

					if(player.GetComponent<OO_PlayerManager>().enemiesToKill <= 0)
					{
						player.GetComponent<DC_CamTransition>().Start3rdTransition(); // transition the camera view back to the initial 3rd person
					}
				}

				else if(player.GetComponent<OO_PlayerManager>().enemiesToKill <= 0 && other.gameObject.GetComponent<OO_EnemyScript>().fl_minhealth <= 0) // if enemy has 0 health
				{
					Destroy(other.gameObject); // destroy the enemy after health has reached 0
					player.GetComponent<OO_PlayerManager>().enemiesToKill2 -= 1; // check for the number of enemies and subtract 1 from the number

					if(player.GetComponent<OO_PlayerManager>().enemiesToKill2 <= 0)
					{
						player.GetComponent<DC_CamTransition>().Start3rdTransition(); // transition the camera view back to the initial 3rd person
					}
				}
				Destroy(this.gameObject); // destory the bullet when it hits something
			}
			else if(other.gameObject.GetComponent<OO_EnemyCharacter>() != null) // when you shoot the character
			{
				GetComponent<SphereCollider>().enabled = false;
				other.gameObject.GetComponent<OO_EnemyCharacter>().fl_minhealth -= 10;

				if(other.gameObject.GetComponent<OO_EnemyCharacter>().fl_minhealth <= 0 && player.GetComponent<OO_PlayerManager>().enemiesToKill >= 1) // if enemy has 0 health
				{
					GameObject bloodeffect = (GameObject)Instantiate (other.GetComponent<OO_EnemyCharacter>().go_enemyBloodEffect, 
					                                                  other.GetComponent<OO_EnemyCharacter>().tr_bloodEffectPoint.position, other.GetComponent<OO_EnemyCharacter>().tr_bloodEffectPoint.rotation); // instantiate blood

					Destroy(other.gameObject); // destroy the enemy after health has reached 0
					player.GetComponent<OO_PlayerManager>().enemiesToKill -= 1; // check for the number of enemies and subtract 1 from the number

					if(player.GetComponent<OO_PlayerManager>().enemiesToKill <= 0)
					{
						player.GetComponent<DC_CamTransition>().Start3rdTransition(); // transition the camera view back to initial 3rd person 
					}
				}
				else if(other.gameObject.GetComponent<OO_EnemyCharacter>().fl_minhealth <= 0 && player.GetComponent<OO_PlayerManager>().enemiesToKill <= 0) // if enemy has 0 health
				{
					GameObject bloodeffect = (GameObject)Instantiate (other.GetComponent<OO_EnemyCharacter>().go_enemyBloodEffect, 
					                                                  other.GetComponent<OO_EnemyCharacter>().tr_bloodEffectPoint.position, other.GetComponent<OO_EnemyCharacter>().tr_bloodEffectPoint.rotation); // instantiate blood
					
					Destroy(other.gameObject); // destroy the enemy after health has reached 0
					player.GetComponent<OO_PlayerManager>().enemiesToKill2 -= 1; // check for the number of enemies and subtract 1 from the number
					
					if(player.GetComponent<OO_PlayerManager>().enemiesToKill2 <= 0)
					{
						player.GetComponent<DC_CamTransition>().Start3rdTransition(); // transition the camera view back to initial 3rd person 
					}
				}
				Destroy(this.gameObject); // destory the bullet when it hits something
			} 
		}
	}
}
