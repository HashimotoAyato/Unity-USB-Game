using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstMenuManager : MonoBehaviour {

	[SerializeField]GameObject FirstMenu;
	TitleManager tm;
	[SerializeField]Button tutorialButton;
	[SerializeField]Button stageSelectButton;

	private const float WAIT_TIME = 0.5f; //チャタリング防止用待機時間
	private float time;
	// Use this for initialization
	void Start () {
		tm = GameObject.Find ("StageManager").GetComponent<TitleManager> ();
		tutorialButton.Select ();
		time = 0.0f;

	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > WAIT_TIME) {
			if (FirstMenu.activeSelf && (Input.GetKey (KeyCode.B) || Input.GetButton ("PS3batsubutton"))) {
				tm.PlayBackSound ();
				FirstMenu.SetActive (false);
				this.gameObject.SetActive (false);
				TitleManager.nowTitle = true;
				//FirstMenuDestroy();
			}
		}
	}

	void OnEnable(){
		time = 0.0f;
		tutorialButton.Select ();
	}

	public void FirstMenuDestroy(){
		Destroy (this.gameObject);
	}
}
