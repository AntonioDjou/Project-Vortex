using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OO_EnemyCharacter : MonoBehaviour 
{
	[Header("Unity Setup Field")]

	public Transform tr_player; 
	public Transform tr_playerHead;
	public GameObject go_Enemybullet;
	public Transform tr_bulletSpawnPoint;
	public GameObject go_enemyBloodEffect;
	public Transform tr_bloodEffectPoint;
	public ParticleSystem psys_muzzleFlash;
	Animator anim_ElyAnimator;
	Animator anim_SciFiAnimator;
	GameObject go_Enemy01;
	public bool bl_isShooting = false;

	[Header("Game Attributes")]

	public float fl_minhealth = 100f; // minimum health value for enemy
	public float fl_maxhealth = 100f; // maximum health value for enemy
	string stng_state = "Seeking";
	public GameObject[] go_wayPoints;
	int int_currentWP = 0;
	public float fl_rotateSpeed = 0.2f;
	public float fl_moveSpeed = 0.1f;
	public float fl_Range = 10f;
	public float fl_firRate = 2f;
	private float fl_accuracyToWaypoint = 1f;
	private float fl_shootCountDown = 0f;
	public static OO_EnemyCharacter enemycharacterScript;



	//======================================================================================================================================================================================

	void Start () 
	{
		anim_ElyAnimator = GetComponent<Animator> ();
		anim_SciFiAnimator = GetComponent<Animator> ();
		go_Enemy01 = GameObject.Find ("Enemy01");
		fl_minhealth = fl_maxhealth; // set maximum starting health of the enemy
	}
	
	//======================================================================================================================================================================================

	void Update () 
	{

		Vector3 V3_direction = tr_player.position - this.transform.position; // the direction the player is to the enemy 
		V3_direction.y = 0; //set the y of the direction to zero so we only look at the angle between the player and the enemy on flat surface

		if(stng_state == "Seeking" && go_wayPoints.Length > 0) // if in seeking state and enemy's waypoint is > 0 then perform following animation
		{
			anim_ElyAnimator.SetBool ("Idle", false);
			anim_ElyAnimator.SetBool ("Ely_Walking", true);
			anim_SciFiAnimator.SetBool ("Idle", false);
			anim_SciFiAnimator.SetBool ("SciFi_Walking", true);
		
			if(Vector3.Distance(go_wayPoints[int_currentWP].transform.position, transform.position) < fl_accuracyToWaypoint) // check distance of the enemy to the waypoint if its within the accuracy
			{
				int_currentWP = Random.Range (0, go_wayPoints.Length); // set the waypoints to random
			}

			//rotate enemy towards the waypoint
			V3_direction = go_wayPoints[int_currentWP].transform.position - transform.position;
			this.transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (V3_direction), fl_rotateSpeed * Time.deltaTime);
		}

		// If distance between player and enemy is less than range, and enemy looking at angle 90 or enemy state is "Seeking"
		if(Vector3.Distance(tr_player.position, this.transform.position) < fl_Range && stng_state == "Seeking") 
		{
			stng_state = "Seeking"; //enemy state is "Seeking"

			//rotate the enemy towards the direction of the player using Quaternion.Slerp
			V3_direction = tr_player.position - transform.position;
			Quaternion Q_rotation = Quaternion.LookRotation (V3_direction);
			transform.rotation = Quaternion.Slerp (transform.rotation, tr_player.rotation, fl_rotateSpeed * Time.deltaTime);
			transform.rotation = Q_rotation;

			if(V3_direction.magnitude > 10) // if distance between enemy and player is greater than 10 then enemy keep walking
			{
				this.transform.Translate (0, 0, fl_moveSpeed * Time.deltaTime);
				anim_ElyAnimator.SetBool ("Ely_Walking", true);
				anim_ElyAnimator.SetBool ("Ely_Shooting", false);
				anim_SciFiAnimator.SetBool ("SciFi_Walking", true);
				anim_SciFiAnimator.SetBool ("SciFi_Shooting", false);

			}
			else
			{
				bl_isShooting = true;
			}
		}
		else
		{
			anim_ElyAnimator.SetBool ("Ely_Walking", true);
			anim_ElyAnimator.SetBool ("Ely_Shooting", false);
			anim_SciFiAnimator.SetBool ("SciFi_Walking", true);
			anim_SciFiAnimator.SetBool ("SciFi_Shooting", false);

			stng_state = "Seeking"; // keep patrolling on waypoints
			return;
		}

		if(bl_isShooting == true) //if enemy can shoot perform enemy shooting animation
		{
			anim_ElyAnimator.SetBool ("Ely_Walking", false);
			anim_ElyAnimator.SetBool ("Ely_Shooting", true);
			anim_SciFiAnimator.SetBool ("SciFi_Walking", false);
			anim_SciFiAnimator.SetBool ("SciFi_Shooting", true);

			if(Time.time >= fl_shootCountDown)
			{
				Shoot ();
				fl_shootCountDown = Time.time + 1f / fl_firRate;
			}
		}
	}
		
	//======================================================================================================================================================================================

	void Shoot()
	{
		psys_muzzleFlash.Play ();

		GameObject bullet = (GameObject)Instantiate (go_Enemybullet, tr_bulletSpawnPoint.position, tr_bulletSpawnPoint.rotation); //public casting the bullet instantiating into a gameobject
		OO_Bullet2.charenemybulletScript = bullet.GetComponent<OO_Bullet2>();

		// Casting Ray to hit taget 

		RaycastHit hit; //defining what the raycast will be hitting
		if(Physics.Raycast(tr_bulletSpawnPoint.transform.position, tr_bulletSpawnPoint.transform.forward, out hit, fl_Range)) // get the starting position of raycast and point it forward store the hit object
		{
			Debug.Log (hit.transform.name); // identify what the raycast hit 
			OO_PlayerManager.playerManagerScript = hit.transform.GetComponent<OO_PlayerManager> (); //getcomponent of the object the raycast hit 

			if(OO_PlayerManager.playerManagerScript != null) // check if the componenet on the hit object exist 
			{
				OO_PlayerManager.playerManagerScript.fl_minHealth -= 1; // take damage
			}
		}
	}
		
	//=====================================================================================================================================================================================

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, fl_Range);
	}
	//=======================================================================================================================================================================================
}
