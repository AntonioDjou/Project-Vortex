using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]

public class OO_Timer : MonoBehaviour 
{
    private GameObject UICanvas;
	[Header("Unity Setup Field")]
	public Text txt_Timer;
	public AudioClip aud_timePickup;
	AudioSource aud_audio;
	public Image img_fillbar;

	[Header("Attributes")]
	public float fl_countDownTimer = 0f;
	public float fl_addonTime = 0;

	public static OO_Timer timerScript;

	//===========================================================================================================================================================

	void Start()
	{
        UICanvas = GameObject.Find("InGame_UI_Canvas/ProgressBar_Background");
		txt_Timer.GetComponent<Text>();
		timerScript = this;
		aud_audio = GetComponent<AudioSource> ();
	}

	//=========================================================================================================================================================

	void Update()
	{
        if (DC_CamTransition.ThirdPerson == true && DC_CamTransition.Transitioning == false)
        {
            fl_countDownTimer -= Time.deltaTime;
            txt_Timer.text = fl_countDownTimer.ToString("0");
            img_fillbar.fillAmount -= 0.3f / fl_countDownTimer * Time.deltaTime; //Reduce the fill amount over countDownTimer
        }
        else // doesn't count down if in first
        {
            UICanvas.GetComponent<Image>().color = new Color32(180, 0, 200, 75);
            // UICanvas.GetComponent<Image>().enabled = false;
            UICanvas.transform.GetChild(0).GetComponent<Image>().color = new Color32(65, 245, 30, 75);
            //UICanvas.transform.GetChild(0).GetComponent<Image>().enabled = false;
            UICanvas.transform.GetChild(1).GetComponent<Image>().color = new Color32(215, 215, 215, 75);
            //UICanvas.transform.GetChild(1).GetComponent<Image>().enabled = false;
            //UICanvas.transform.GetChild(2).GetComponent<Text>().color = new Color32(215, 215, 215, 100);
            //UICanvas.transform.GetChild(2).GetComponent<Text>().enabled = false;
        }

        if (fl_countDownTimer <= 0)
		{
			fl_countDownTimer = 0f;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0; //set timescale to 0 to enable GameOverLoadScreen
		}
	}
		
	//==========================================================================================================================================================

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.tag == "time")
		{
			fl_countDownTimer += fl_addonTime;
			img_fillbar.fillAmount += fl_addonTime * Time.deltaTime;
			aud_audio.PlayOneShot (aud_timePickup, 1f);
			Destroy (other.gameObject);
		}
	}
}
