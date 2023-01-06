using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC_CamTransition : MonoBehaviour 
{
	public static DC_CamTransition camTransitionScript; //make this script public so it can be accessable by other scripts
	public static bool Transitioning = false; // for use when transition to stop clocks and other things of such
	public static bool ThirdPerson = true; // to know which controls to use

	Camera mainCamera;
	GameObject thirdLoc; // empty for third person
	GameObject firstLoc; // emptp for first person
	GameObject enemyLoc; // empty gameobject for enemy
	[Range (2f,4f)]
	public float transitionTime;
	private GameObject player;
    private GameObject crosshair;
    public GameObject weapon;

	//[SerializeField]
	//GameObject lookAtEnemy;

	//===============================================================================================================================================================

	void Awake()
	{
        Cursor.visible = false; // turn off cursor
        Cursor.lockState = CursorLockMode.Locked; // lock the cursor to middle

        mainCamera = GameObject.Find("Exo_Gray/Camera").GetComponent<Camera>();
		thirdLoc = GameObject.Find("Exo_Gray/3rd_Camera");
		firstLoc = GameObject.Find("Exo_Gray/Exo_HiRes_Meshes/EXO_HeadMask/1st_Camera");
		enemyLoc = GameObject.Find("Exo_Gray/Enemy_Cam");
		player = GameObject.Find("Exo_Gray");
        crosshair = GameObject.Find("InGame_UI_Canvas/Crosshair");
		camTransitionScript = this;
	}
	//=================================================================================================================================================================

    void Start()
    {
        crosshair.SetActive(false);
    }
	//=========================================================================================================================================================================

	public void Start1stTransition()
	{
		enemyLoc.transform.LookAt(enemyLoc.transform); // makes empty look at enemy
		InvokeRepeating("CamTransitionToEnemy", 0f, Time.deltaTime);
	}
	//============================================================================================================================================================================

	public void Start3rdTransition()
	{
		InvokeRepeating("CamTransition1stTo3rd", 0f, Time.deltaTime);
	}
	//===============================================================================================================================================================================

	//transition from enemy to first
	void CamTransitionEnemyTo3rd()
	{
		//print (mainCamera.transform.localPosition.y);
		mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, thirdLoc.transform.position, Time.deltaTime * transitionTime);
		mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, thirdLoc.transform.rotation, Time.deltaTime * transitionTime);
		
		if(mainCamera.transform.localPosition.y >= 2.53f)
		{
			CancelInvoke();
			mainCamera.transform.position = thirdLoc.transform.position; // set position 
			InvokeRepeating("CamTransitionTo1st", 0f, Time.deltaTime);
		}
    }
    //=================================================================================================================================================================================

    //move camera from first person to third
    void CamTransitionTo1st()
	{
		//print (mainCamera.transform.localPosition.y);
		mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, firstLoc.transform.position, Time.deltaTime * transitionTime);
		mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, firstLoc.transform.rotation, Time.deltaTime * transitionTime);

        //if (mainCamera.transform.localPosition.y <= (firstLoc.transform.localPosition.y + 0.5f))
        if (mainCamera.transform.localPosition.y <= 1.66f)
		{
            crosshair.SetActive(true);
            weapon.SetActive(true); // turn on weapon
            ThirdPerson = false;
			CancelInvoke();
			//player.GetComponent<Animator>().enabled = true; // turn back on player movement
			Transitioning = false;
			mainCamera.transform.position = firstLoc.transform.position; // set position
            mainCamera.transform.parent = this.transform.GetChild(0).transform.GetChild(5).transform.GetChild(0);
		}
        Cursor.visible = false; // turn off cursor
        Cursor.lockState = CursorLockMode.Locked; // lock the cursor to middle
    }
    //=================================================================================================================================================================================

    // transition back to third person
    void CamTransition1stTo3rd()
	{
        //print(mainCamera.transform.localPosition.y);
        weapon.SetActive(false); // turn on weapon
        mainCamera.transform.parent = GameObject.Find("Exo_Gray").transform; // reset the parent
		//player.GetComponent<OO_Controls_Animations>().TurnOffCrossHair();
		mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, thirdLoc.transform.position, Time.deltaTime * transitionTime); // change position
		mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, thirdLoc.transform.rotation, Time.deltaTime * transitionTime); // change rotation

		if(mainCamera.transform.localPosition.y >= 2.54f)
		{
            crosshair.SetActive(false);
            ThirdPerson = true; // now in third
			Transitioning = false; // reset bool
			CancelInvoke(); // turn off lerp
			mainCamera.transform.position = thirdLoc.transform.position; // set position to third camera
			player.GetComponent<Animator>().enabled = true; // turn back on pplayer movement
            player.GetComponent<OO_Controls_Animations>().ResetCamHolder();
		}
	}
	//=================================================================================================================================================================================

	void CamTransitionToEnemy()
	{
		if(mainCamera.transform.localPosition.y <= 1.851f) // if camera is at location
		{
			CancelInvoke();
			mainCamera.transform.position = enemyLoc.transform.position; // set position 
			InvokeRepeating("CamTransitionEnemyTo3rd", 0f, Time.deltaTime);
		}

		if(mainCamera.transform.localPosition.y != enemyLoc.transform.localPosition.y)
			//mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, lookAtEnemy.transform.position, Time.deltaTime * transitionTime);
			mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, enemyLoc.transform.position, Time.deltaTime * transitionTime);

		if(mainCamera.transform.rotation != enemyLoc.transform.rotation)
			//mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, lookAtEnemy.transform.rotation, Time.deltaTime * transitionTime);
			mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, enemyLoc.transform.rotation, Time.deltaTime * transitionTime);
	}
	//====================================================================================================================================================================================
}
