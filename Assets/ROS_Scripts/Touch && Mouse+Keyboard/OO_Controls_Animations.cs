using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OO_Controls_Animations : MonoBehaviour 
{
	//assigning variable for animator 
	internal Animator animator;

	public Transform tf_climbTarget = null; 

	public Transform jumpTarget = null; 

	public static OO_Controls_Animations controlAnimationScript;

	//Calling the controller for our character controller
	public CharacterController cc_Controller; 
	// for first person movement
	private Vector3 moveDirection = Vector3.zero;
	float moveSpeed = 5; // first person move speed

	public GameObject bullet;
	[SerializeField]
	[Range (25000,30000)]
	int bulletSpeed = 50;
	[SerializeField]
	float bulletLife = 1;
	public Camera camera;
	public Transform lookat;

	float rollleft = Animator.StringToHash ("Base Layer.RollLeft");
	float rollright = Animator.StringToHash ("Base Layer.RollRight");
	float rollback = Animator.StringToHash ("Base Layer.RollBack");
	float jump = Animator.StringToHash ("Base Layer.Jump");
	float slide = Animator.StringToHash ("Base Layer.Slide");
	float sprintleft = Animator.StringToHash ("Base Layer.SprintLeft");
	float sprintright = Animator.StringToHash ("Base Layer.SprintRight");

	public float DirectionDampTime = 25f;

	float h; 
	float v;

    [SerializeField]
    float rotateSpeed;
    [SerializeField]
    GameObject cameraHolder;
    GameObject shootArea;
	[Space]

	[Header("Checking Player Gounded")]
	[Space]
	public bool bl_isGrounded = true;

	//==============================================================

	void Start ()
	{
		//Getting reference to the CharacterController component 
		cc_Controller = GetComponent<CharacterController>();

		controlAnimationScript = this;
		
		//making reference to the animator component 
		animator = GetComponent<Animator>();
				Time.timeScale = 1;

        shootArea = GameObject.Find("Exo_Gray/Exo_HiRes_Meshes/EXO_HeadMask/1st_Camera/Bullet_Zone");
	}
	
	//===========================================================

	void Update ()
	{
		if(DC_CamTransition.ThirdPerson)
		{
			RollingLeft ();
			RollingRight();
			RollingBack ();
			Jumping();
			Sliding();
			Vaulting();
			SprintingLeft();
			SprintingRight();
			
			v = Input.GetAxis ("Horizontal");
			h = Input.GetAxis ("Horizontal");
			
			if(animator.GetBool("RollLeft")==false 
			   && animator.GetBool("RollRight")==false 
			   && animator.GetBool("RollBack")==false 
			   && animator.GetBool("Slide")==false)
			{
				cc_Controller.height = 2;
				cc_Controller.center = new Vector3(cc_Controller.center.x,1.03F,cc_Controller.center.z);
			}
			else
			{
				cc_Controller.height = 1;
				cc_Controller.center = new Vector3(cc_Controller.center.x,0.52F,cc_Controller.center.z);
			}
		}
		else
		{
			FirstPersonMove();
			//Vector2 center = new Vector2(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2); // gets mouse position by subtracting screen size and width

			if(Input.GetButtonDown("Fire1"))
			{
				RaycastHit hit;
				Ray myRay = camera.ScreenPointToRay(Input.mousePosition);
				Physics.Raycast(myRay, out hit, 20);
				
				Vector3 target = myRay.GetPoint(10);
                //GameObject test = Instantiate(bullet, target, Quaternion.identity); // creat object for empty to look at
                //test.GetComponent<Renderer>().enabled = false; // make it invisible
                //Destroy(test.gameObject, 0.25f); // destroy it

                //transform.GetChild(8).transform.LookAt(test.transform); // look at mouse direction
                //GameObject Bullet = Instantiate(bullet, transform.GetChild(8).transform.position, transform.GetChild(8).transform.rotation);

                GameObject Bullet = Instantiate(bullet, shootArea.transform.position, shootArea.transform.rotation);
				Bullet.GetComponent<Rigidbody>().AddForce(Bullet.transform.forward * bulletSpeed * Time.deltaTime); // move the bullet forward
				Bullet.name = "bullet"; // change bullet name
				Destroy(Bullet.gameObject, bulletLife);
			}
		}
		//time it takes to scale the collider in real time
		//GetComponent<Animator>().enabled = false;
	}
	//========================================================================

	void FixedUpdate()
	{
		animator.SetFloat ("RollLeft", rollleft);
		animator.SetFloat ("RollRight", rollright);
		animator.SetFloat ("RollBack", rollback);
		animator.SetFloat ("Slide", slide);
		animator.SetFloat ("Sprint", v );
	}
	//=============================================================================

	public void RollingLeft()
	{
		cc_Controller.height = 2;
		if(ETCInput.GetButton ("Rollleft_button") || (Input.GetKey(KeyCode.Q)))  
		{
			Debug.Log ("You are now rolling left!");
			animator.SetBool ("RollLeft", true);
		}
		else
		{
			animator.SetBool ("RollLeft", false);
		}
	}
	//==========================================================================================================
	
	public void RollingRight()
	{
		cc_Controller.height = 2;

		if(ETCInput.GetButton ("Rollright_button") || (Input.GetKey(KeyCode.E)))  
		{
			cc_Controller.height = 1;
			animator.SetBool ("RollRight", true);
		}
		else
		{
			animator.SetBool ("RollRight", false);
		}
	}
	//====================================================================================================
	
	public void RollingBack()
	{
		cc_Controller.height = 2;

		if(ETCInput.GetButton ("Rollback_button") || (Input.GetKey(KeyCode.S)))  
		{
			cc_Controller.height = 1;
			animator.SetBool ("RollBack", true);
		}
		else
		{
			animator.SetBool ("RollBack", false);
		}
		
		Ray ray = new Ray(transform.position + Vector3.up, - Vector3.up);
		RaycastHit hitInfo = new RaycastHit();
		
		if(Physics.Raycast (ray, out hitInfo))
		{
			//if the distance to the ground is more than 1.75, use the Match Target
			if(hitInfo.distance > 1.75f)
			{
				animator.MatchTarget (hitInfo.point, Quaternion.identity, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(0,1,0),0),0.35f, 0.8f);
			}
		}
	}
	//=========================================================================
	public void Jumping()
	{
		if(ETCInput.GetButton("Jump_button") || (Input.GetKey (KeyCode.Space)))
		{
			animator.SetBool ("Jump", true);
		}
		else
		{
			animator.SetBool ("Jump", false);
		}
	}

	//========================================================================================================
	
	public void Sliding()
	{
		if(ETCInput.GetButton ("Slide_button") || (Input.GetKey(KeyCode.W))) 
		{
			cc_Controller.height = 1;
			Debug.Log ("scales to 1");
			animator.SetBool ("Slide", true);
		}
		else
		{
			animator.SetBool("Slide", false);
		}

	}
	//=====================================================================================================

	public void Vaulting()
	{
		if(animator)
		{
			AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
			if(ETCInput.GetButtonDown ("Vault_button")) animator.SetBool ("Vault", true); //(Input.GetKeyDown (KeyCode.O))
			
			if(state.nameHash == Animator.StringToHash ("Base Layer.Vaulting"))
			{
				animator.SetBool ("Vault", false);
				animator.MatchTarget(jumpTarget.position, jumpTarget.rotation, AvatarTarget.LeftHand, new MatchTargetWeightMask(Vector3.one, .0f),animator.GetFloat("MatchStart"),animator.GetFloat("MatchEnd"));
			}
		}
	}
	//========================================================================================================

	public void SprintingLeft()
	{
		if(ETCInput.GetButton("Runleft_button"))  //(Input.GetKey (KeyCode.Space))
		{
			animator.SetBool ("SprintLeft", true);
		}
		if(ETCInput.GetButton ("Runleft_button") == false)  //(Input.GetKey (KeyCode.Space) == false)
		{
			animator.SetBool ("SprintLeft", false);
		}
	}

	public void SprintingRight()
	{
		if(ETCInput.GetButton("Runright_button"))  //(Input.GetKey (KeyCode.Space))
		{
			animator.SetBool ("SprintRight", true);
		}
		if(ETCInput.GetButton ("Runright_button") == false)  //(Input.GetKey (KeyCode.Space) == false)
		{
			animator.SetBool ("SprintRight", false);
		}
	}
	public void ReturnToGame()
	{
		GetComponent<Animator>().enabled = true;
	}
	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "CamTrigger" && DC_CamTransition.ThirdPerson) // only if you're not in third person
		{
			//collider.gameObject.SetActive(false);
			GetComponent<Animator>().enabled = false;
			DC_CamTransition.Transitioning = true; // you are transitioning so tiem stops
			GetComponent<DC_CamTransition>().Start1stTransition();
			//GetComponent<DC_CamTransition>().Start1stTransition(collider.gameObject);
		}
	}
	// ----------- Daniel ---- Code -------------
	void FirstPersonMove()
	{
		// move player
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = cameraHolder.transform.TransformDirection(moveDirection); // changes the character to move forward from cameraholder
		moveDirection *= moveSpeed;
		cc_Controller.Move(moveDirection * Time.deltaTime);

        //rotate camera
        float verticalMouseInput = Input.GetAxis("Mouse Y");  // get the change in mouse position
        float newCameraAngle = cameraHolder.transform.eulerAngles.x + verticalMouseInput;  // calculate the new angle

        if (newCameraAngle < 30.0f || newCameraAngle > 330.0f) // -30
        {
            cameraHolder.transform.Rotate(-verticalMouseInput * rotateSpeed * Time.deltaTime, 0, 0); // if the new angle is within our window, move the camera
        }
        if (newCameraAngle <= 330.0f && newCameraAngle > 150) // -30
        {
            cameraHolder.transform.eulerAngles = new Vector3(331.5f, cameraHolder.transform.eulerAngles.y, cameraHolder.transform.eulerAngles.z);
        }
        if (newCameraAngle >= 30.0f && newCameraAngle <150)
        {
            cameraHolder.transform.eulerAngles = new Vector3(28.5f, cameraHolder.transform.eulerAngles.y, cameraHolder.transform.eulerAngles.z);
        }
        // apply horizontal mouse movement
        float horizontalMouseInput = Input.GetAxis("Mouse X");
        float newCameraX = cameraHolder.transform.localEulerAngles.y + horizontalMouseInput;

        //print(cameraHolder.transform.eulerAngles.y);
       // print(newCameraX);

        //300 & 60
        if (newCameraX > 300 || newCameraX < 60) // if greater than 235 or less than 305
        {
            cameraHolder.transform.Rotate(0, horizontalMouseInput * rotateSpeed * Time.deltaTime, 0);
        }
        if (newCameraX < 235)
        {
            //cameraHolder.transform.eulerAngles = new Vector3(cameraHolder.transform.eulerAngles.x, 236, cameraHolder.transform.eulerAngles.z);
        }
        else if (newCameraX < 305)
        {
            //cameraHolder.transform.Rotate(0, horizontalMouseInput * rotateSpeed * Time.deltaTime, 0);
        }
        //transform.Rotate(0, horizontalMouseInput * rotateSpeed * Time.deltaTime, 0);
    }
	public void TurnOnCrossHair()
	{
		//Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
	}
	public void TurnOffCrossHair()
	{
		//Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
	}
    public void ResetCamHolder()
    {
        cameraHolder.transform.eulerAngles = new Vector3(0, -90, 0);
    }
}
