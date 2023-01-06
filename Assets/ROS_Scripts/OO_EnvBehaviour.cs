using UnityEngine;
using System.Collections;

public class OO_EnvBehaviour : MonoBehaviour 
{
	public GameObject go_Portal;
	public float fl_moveSpeed = 5.0f;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.up * fl_moveSpeed * Time.deltaTime);
	}

	
}
