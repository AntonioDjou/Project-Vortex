using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]


public class OO_Tutorial : MonoBehaviour 
{
	[Space]
	[Header("Tutorial Audio")]
	public AudioClip aud_Clips;
	AudioSource aud_audioSouce;

	[Space]
	public float fl_playerDistanceToTutorialTriggers = 100f;
	[Space]

	[Header("Player")]
	[Space]
	public GameObject go_player; 

	// Use this for initialization
	void Start () 
	{
		aud_audioSouce = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float fl_dist = Vector3.Distance (go_player.transform.position, transform.position);

		if(fl_dist <= fl_playerDistanceToTutorialTriggers)
		{
			aud_audioSouce.PlayOneShot (aud_Clips,0.5f);
		}
	}
}
