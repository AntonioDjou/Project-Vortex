using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]

public class OO_PlayerManager : MonoBehaviour 
{
	public static OO_PlayerManager playerManagerScript;

	[Header("Audio, Animations")]
	[Space]
	public AudioClip aud_orbsPickup;
	AudioSource aud_audio;
	public Animation anim_cameShakeAnim; // animation for the main camera to shake
	Animator anim_playerAnimator;

	[Header("Unity Setup UIs")]
	[Space] 
	public Image img_healthBar;
	public Image img_strengthFillBar; // slider image that represents the player's strength value.
	public Text txt_checkPointTimer; // Text to display player restarting on checkpoint timer

	[Header("Player's Health")]
	[Space]
	public float fl_minHealth = 100;
	public float fl_maxHealth = 100;
	public float fl_obstacleDamage = 5f;

	[Space]
	public GameObject go_shieldParticle;
	public GameObject go_healingParticle;
	public Vector3 V3_checkPointPosition = new Vector3(0, 0, 0);

	[Space]
	public bool bl_activateShield = false;
	public bool bl_isEmitter = false;
	public bool bl_canBreakWall = false;
	public bool bl_playerIsDead = false;

	[Header("Attributes")]
	[Space]
	public float fl_shieldActiveTime = 10f; // shield activation timer, controls how long the shield takes to inactive
	public float fl_emitterTimer = 2f;
	public float fl_wallBreakingTime = 0f; // timer that control the progress of the strength value.
	public float fl_playerRestartTime = 3f;
	
	public Color clr_maxColor;
	public Color clr_minColor;

	[Header ("Number of enemies in level")]
	public int enemiesToKill, enemiesToKill2;

	//========================================================================================================================================================================
	void Start () 
	{
		anim_playerAnimator = GetComponent<Animator> (); //getcomponent reference for the animator 
		anim_cameShakeAnim = GetComponent<Animation> (); //getcomponent reference to the animation for the cameraShake
		aud_audio = GetComponent<AudioSource> (); // get the component of the audioSource for the audioClip that will be played
		txt_checkPointTimer.GetComponent<Text> ().enabled = false;
		playerManagerScript = this;
		fl_minHealth = fl_maxHealth; // set minimum health value to = the maximum health value
		go_shieldParticle.SetActive (false);
		go_healingParticle.SetActive (false);
		anim_playerAnimator.SetBool ("Death", false);
    }

    //==========================================================================================================================================================================

    void Update () 
	{
		float ratio = fl_minHealth / fl_maxHealth;
		img_healthBar.rectTransform.localScale = new Vector3 (ratio, 1, 1);

		if(bl_activateShield == true) //if the activeshield boolean is true then begin coutdown time
		{
			fl_shieldActiveTime -= Time.deltaTime;
			if(fl_shieldActiveTime <= 0f) //if countdown time = 0 set activateshield boolean to false then deactivate the shield 
			{
				bl_activateShield = false; //set activateshield boolean to false
				go_shieldParticle.SetActive (false); //deactivate shield 
				fl_shieldActiveTime = 0f;
			}
		}

		if(bl_isEmitter) //if boolean to emitt the particle effects is true start the timer for the particle effect
		{
			fl_emitterTimer -= Time.deltaTime;
			if(fl_emitterTimer <= 0f) //if emitter timer is <= 0 then the boolean emitter is false and deactive all particle effects
			{
				bl_isEmitter = false;
				go_healingParticle.SetActive (false); // deactivate healing particle effect
			}
		}

		if(bl_canBreakWall)
		{
			fl_wallBreakingTime -= Time.deltaTime; // time to wallbreak timer -= time in real time and start counting down
			img_strengthFillBar.fillAmount -= 0.3f / fl_wallBreakingTime * Time.deltaTime; // Reduce the strength value by 0.3 over wallbreaking-timer 

			if(fl_wallBreakingTime <= 0) 
			{
				bl_canBreakWall = false;
				fl_wallBreakingTime = 0;
				img_strengthFillBar.fillAmount = fl_wallBreakingTime;
			}
		}
		if(!bl_canBreakWall)
		{
			fl_wallBreakingTime = 10f;
		}
			
		RestartOnCheckPoint(); //routine function to retsrt player from checkpoint after playerdeath animation.
		Death(); // Death function 
	}

	void Death ()
	{
		if(fl_minHealth <= 0 ) //if player health is below 0 
		{
			bl_playerIsDead = true;
			gameObject.transform.position = V3_checkPointPosition; // set player restart position to the last checkpoint position and....
			this.GetComponent<DC_CamTransition>().Start3rdTransition(); // Transition the camera back to the 3rd person 
		}
	}

	void RestartOnCheckPoint()
	{
		if(transform.position == V3_checkPointPosition)
		{
			fl_playerRestartTime -= Time.deltaTime; // Decrease restart time in real time time 
			txt_checkPointTimer.text = fl_playerRestartTime.ToString("0"); // Set playerrestarttime to sting number 
			txt_checkPointTimer.GetComponent<Text> ().enabled = true; // Display retsart timer text on Screen 
			this.GetComponent<Animator>().enabled = false;
			this.GetComponent<OO_Timer>().fl_countDownTimer = 0f; // reset level countdown timer to initial time

			fl_minHealth = 100; // if player's restart position is the last checkpoint position set player's health back to max health

			if(fl_playerRestartTime <= 0) //if playerrestarttime is = 0 enable animator to restart running state
			{
				this.GetComponent<Animator>().enabled = true; // set animator component on player to true
				fl_playerRestartTime = 3f; // reset deathrestart time after reaching 0
				this.GetComponent<OO_Timer>().fl_countDownTimer = 30f; // reset level countdown timer to initial time
				txt_checkPointTimer.GetComponent<Text> ().enabled = false; // Disable retsart timer text on Screen 
			}
			bl_playerIsDead = false; // playerdeath = false
		}
	}

	//========================================================================================================================================================================

	void OnTriggerEnter (Collider other)
	{
		if(other.CompareTag("dangerous"))
        {
			bl_isEmitter = true; // boolean to set the emitter particle is true
			if(!bl_activateShield) //otherwise if activateshield boolean is false then -5 from player's health
			{
				fl_minHealth -= fl_obstacleDamage; //Do -5 damage to the player 
				anim_cameShakeAnim.Play(); //play the cameraShake animation
				anim_cameShakeAnim["Camera_Shake"].speed = 1f;
			}
			fl_emitterTimer = 2f; // reset the emitter timer to the initial time

			if(fl_minHealth < 0) //if the player's minimum health is less than 0, then minimum health = 0 
			{
				fl_minHealth = 0; //set mminimum  health to 0
			}
		}

		if(other.CompareTag("lifegenerate") )
		{
			bl_isEmitter = true;
			fl_minHealth += 10;
			go_healingParticle.SetActive (true);
			aud_audio.PlayOneShot (aud_orbsPickup, 1f);

			if(fl_minHealth > fl_maxHealth)
			{
				fl_minHealth = fl_maxHealth;
			}
			fl_emitterTimer = 2f;
		}

		if(other.CompareTag("shieldtrigger"))
		{
			bl_activateShield = true; //if gameobject that enter shieldpickup is the player, then set gameobject shield to active
			go_shieldParticle.SetActive (true); //set shield active
			Destroy (other.gameObject); //Destroy the first trigger
			fl_shieldActiveTime = 10f; //Restart Shield Timer for the next pickup (Antônio)
        }

		if(other.CompareTag("Power"))
		{
			bl_canBreakWall = true;
			other.gameObject.SetActive (false);
			img_strengthFillBar.fillAmount = 10f;
		}	
	}
	//================================================================================================================================================================
}
