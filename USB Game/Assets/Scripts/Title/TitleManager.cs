using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour {
	//[SerializeField]GameObject menuWindow;
	[SerializeField]GameObject firstMenu;
	[SerializeField]GameObject canvas;
	[SerializeField]Button tutorialButton;
	[SerializeField] AudioClip StartSE;
	[SerializeField]AudioClip backButtonSE;

	GameObject temp;

	public static bool nowTitle;
	// Use this for initialization
	void Start () {
		nowTitle = true;
		//firstMenu = (GameObject)Resources.Load ("menu/FirstSelectMenu");
	}
	
	// Update is called once per frame
	void Update () {
		if (nowTitle && (Input.GetKeyDown (KeyCode.Space) || Input.GetButton("PS3startbutton"))) {
			PlayOKSound ();
			nowTitle = false;
			firstMenu.SetActive (true);
			/*temp = Instantiate (firstMenu);
			temp.transform.parent = canvas.transform;
			temp.transform.position = canvas.transform.position;
			temp.transform.position += new Vector3 (0.0f,-54.0f,0.0f);
			*/
		}
	}

	public void StageSelect(int stageNumber){
		StageManager.stageSelect = stageNumber;
		SceneManager.LoadScene ("MainScene");
	}

	public void PlayBackSound(){
		GetComponent<AudioSource> ().PlayOneShot (backButtonSE);
	}

	public void PlayOKSound(){
		GetComponent<AudioSource> ().PlayOneShot (StartSE);
	}
}
