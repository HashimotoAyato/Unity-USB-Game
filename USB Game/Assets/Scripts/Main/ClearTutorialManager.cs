using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearTutorialManager : MonoBehaviour {

	[SerializeField]GameObject nextButton;
	[SerializeField]GameObject returnTitleButton;

	const int LAST_STAGE_NUMBER = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable(){
		if (StageManager.stageSelect == LAST_STAGE_NUMBER) {
			nextButton.SetActive (false);
			returnTitleButton.GetComponent<Button> ().Select ();
		} else {
			nextButton.GetComponent<Button> ().Select ();
		}
	}
}
