using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OO_Bullet2 : MonoBehaviour 
{
	public static OO_Bullet2 charenemybulletScript;

	public float fl_bulletDestroyTime = 1f;
	public float fl_bulletSpeed = 50f;


	//===========================================================================================================================================================================
	void Start()
	{
		charenemybulletScript = this;
	}
		
	//================================================================================================================================================================================

	void Update () 
	{
		if(OO_EnemyCharacter.enemycharacterScript.tr_player == null) //if there is no more target to hit then destroy the bullet gameobject
		{
			Destroy (gameObject, fl_bulletDestroyTime);
			return;
		}

		//find direction for the bullet to look at its target
		Vector3 V3_direction = OO_EnemyCharacter.enemycharacterScript.tr_player.position - transform.position;
		float fl_distanceThisFrame = fl_bulletSpeed * Time.deltaTime;

		if(V3_direction.magnitude <= fl_distanceThisFrame)
		{
			//HitTarget ();
			return;
		}

		transform.Translate (V3_direction.normalized * fl_distanceThisFrame, Space.World);
	}

	//=========================================================================================================================================================

	public void SeekTarget(Transform _target)
	{
		OO_EnemyCharacter.enemycharacterScript.tr_player = _target;
	}

	//==========================================================================================================================================================================

//	void HitTarget ()
//	{
//		Destroy (gameObject, fl_bulletDestroyTime);
//		if(OO_PlayerManager.playerManagerScript.bl_activateShield == false)
//		{
//			OO_PlayerManager.playerManagerScript.fl_minHealth -= 5;
//		}
//	}

	//==============================================================================================================================================================================


}
