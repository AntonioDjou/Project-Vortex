using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OO_EnemyScript : MonoBehaviour 
{
	public Transform tr_target;
	public static OO_EnemyScript enemyScript;

	[Header("Attributes")]

	public float fl_minhealth = 100f; // minimum health for the enemy 
	public float fl_maxhealth = 100f; // maximum health for the enemy 
	public float fl_range = 4f; 
	public float fl_rotationSpeed = 5f;
	public float fl_fireRate = 1f;
	private float fl_shootCountdown = 0f;

	[Header("Setup Field")]

	public string stng_playerTag = "Player";
	public Transform tr_enemyRotationPart;
	public bool bl_shoot = false;
	public GameObject go_bullet;
	public Transform tr_bulletSpawnPoint;
	public ParticleSystem psys_muzzleFlash;


	//=====================================================================================================================================================================================

	void Start () 
	{
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);
		enemyScript = this;
		fl_minhealth = fl_maxhealth; // set the maximum health of the enemy
	}

	//====================================================================================================================================================================================

	void UpdateTarget()
	{
		GameObject[] _player = GameObject.FindGameObjectsWithTag (stng_playerTag);
		float _shortestDistance = Mathf.Infinity; //find and store shortest distance to the player 
		GameObject _nearestPlayer = null; //store the nearest gameobject player find
		foreach(GameObject player in _player)
		{
			float DistanceToPlayer = Vector3.Distance (transform.position, player.transform.position); //getting distance to the player 

			//if distance to our player is less that the shortest distance 
			if(DistanceToPlayer < _shortestDistance) 
			{
				_shortestDistance = DistanceToPlayer; // set shortest distance to the distance of the player 
				_nearestPlayer = player; // nearest player found = our player 
			}
		}

		if(_nearestPlayer != null && _shortestDistance <= fl_range) // if player is detected and shortest distance is within the range
		{
			tr_target = _nearestPlayer.transform; // the closest target is player 
		}
		else
		{
			tr_target = null; // otherwise target is null 
		}
	}

	//====================================================================================================================================================================================

	void Update () 
	{
		if(tr_target == null) // if target which is the palyer does not exist then return and do nothing
		{
			return; // return to update function
		}

		LookAtTarget (); // function that call the enemy attention to look at player

		if(Time.time >= fl_shootCountdown)
		{
			Shoot ();
			fl_shootCountdown = Time.time + 1f / fl_fireRate;
		}

		fl_shootCountdown -= Time.deltaTime;

	}

	//=================================================================================================================================================================================

	void LookAtTarget()
	{
		// enemy lock on the target as the player
		Vector3 dir = tr_target.position - transform.position;
		Quaternion _lookRotate = Quaternion.LookRotation (dir);
		//smooth transition from current rotation to the new rotation over time based on turnspeed 
		Vector3 V3_rotation = Quaternion.Lerp (tr_enemyRotationPart.rotation, _lookRotate, Time.deltaTime * fl_rotationSpeed).eulerAngles; 
		tr_enemyRotationPart.rotation = Quaternion.Euler (0f, V3_rotation.y, 0f);
	}

	//==============================================================================================================================================================================

	void Shoot()
	{
		psys_muzzleFlash.Play (); // play muzzleflash shooting effect
		bl_shoot = true;

		GameObject bullet = (GameObject)Instantiate (go_bullet, tr_bulletSpawnPoint.position, tr_bulletSpawnPoint.rotation); //public casting the bullet instantiating into a gameobject
		OO_Bullet2.charenemybulletScript = bullet.GetComponent<OO_Bullet2>();

		// Casting Ray to hit taget

		RaycastHit hit; //defining what the raycast will be hitting
		if(Physics.Raycast(tr_bulletSpawnPoint.transform.position, tr_bulletSpawnPoint.transform.forward, out hit, fl_range)) // get the starting position of raycast and point it forward store the hit object
		{
			Debug.Log (hit.transform.name); // identify what the raycast hit 
			OO_PlayerManager.playerManagerScript = hit.transform.GetComponent<OO_PlayerManager> (); //getcomponent of the object the raycast hit 

			if(OO_PlayerManager.playerManagerScript != null) // check if the componenet on the hit object exist 
			{
				OO_PlayerManager.playerManagerScript.fl_minHealth -= 1; // take damage
			}
		}
	}

	//========================================================================================================================================================================================

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, fl_range);
	}
	//======================================================================================================================================================================================
}